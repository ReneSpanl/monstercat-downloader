using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;


namespace monstercat_downloader
{
    public partial class Main_Window : Form
    {
        string prgPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        DownloadQueue dq;
        int releaseCount = 0;
        int limitNumber = 50;
        public MCatJSON JDO;
        WebClient wc = new WebClient();
        string currentdir;
        bool con = false;
        Thread dl_thread;
        string extension = "flac";
        bool dl_podcast=false, dl_mixes=false;
        StreamReader reader;
        Stream data;

        public Main_Window()
        {
            InitializeComponent();
            writeDebugLine("Startup");
            dq = new DownloadQueue();
            currentdir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
        }

        private void btn_get_releases_Click(object sender, EventArgs e)
        {
            labelupdater();

        }
        private void writeDebugLine(string str)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                Box_debug.AppendText(str + "\r\n");
            };
            if (this.InvokeRequired)
            {
                this.Invoke(methodInvokerDelegate);
            }
            else
            { 
                methodInvokerDelegate();
            }
        }
        private void update_dl_title(string str)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                lbl_curtitle.Text = str;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(methodInvokerDelegate);
            }
            else
            {
                methodInvokerDelegate();
            }
        }
        private void pbar_updater(int max, int cur)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                pb_dl.Maximum = max;
                pb_dl.Value = cur;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(methodInvokerDelegate);
            }
            else
            {
                methodInvokerDelegate();
            }

        }
        private void writeDebugFile(string str)
        {
            System.IO.File.AppendAllText(@"log.txt","\n" + str);

        }

        private string returnJSONFromURL(string URL)
        {
            writeDebugLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            data = wc.OpenRead(URL);
            reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            data = null;
            reader = null;
            return s;

        }
        public int getReleaseCount()
        {
            writeDebugLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            MCatJSON J = Newtonsoft.Json.JsonConvert.DeserializeObject<MCatJSON>(returnJSONFromURL("https://connect.monstercat.com/api/catalog/browse/?limit=0")) ;
            releaseCount = J.total;
            return J.total;
            

        }
        public void labelupdater()
        {
            con = true;
            writeDebugLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            lbl_rls_count.Text = ("available releases: " + getReleaseCount().ToString());
        }


        public string resultToURL(Result r)
        {
            return "https://connect.monstercat.com/api/release/" + r.albums.albumId.ToString() + "/download?method=download&type=flac&track=" + r._id.ToString();
        }

        private void btn_start_download_Click(object sender, EventArgs e)
        {
            bool _dl_podcast = dl_podcast;
            bool _dl_mixes = dl_mixes;
            if (!con){
                labelupdater();
            }
            if (!Directory.Exists("download"))

            {

                Directory.CreateDirectory("download");

            }
            if (releaseCount == 0)
            {
                MessageBox.Show("could not fetch releaseCount");
                return;
            }

            pb_dl.Maximum = releaseCount;

            MCatJSON J;
            string URLString="";
            string str;
            for (int i = 0; i < (releaseCount / limitNumber) + 1; i++)
            {
                str = "https://connect.monstercat.com/api/catalog/browse/?limit=" + limitNumber.ToString() + "&skip=" + (i * limitNumber).ToString();
                writeDebugLine("Fetch String: " + str);
                J = Newtonsoft.Json.JsonConvert.DeserializeObject<MCatJSON>(returnJSONFromURL(str));
                for (int x = 0; x < J.results.Count; x++)
                {
                    if (J.results[x].streamable == false)
                    {
                        writeDebugFile("skipping URL: " + resultToURL(J.results[x]) + " release not yet downloadable");

                    }
                    else if(_dl_podcast == false && J.results[x].release.type == "Podcast")
                    {
                        writeDebugFile("skipping URL: " + resultToURL(J.results[x]) + " release is a Podcast");
                    }
                    else {
                        URLString = resultToURL(J.results[x]);
                        dq.appendToQueue(new DownloadObject(URLString, J.results[x].artistsTitle  + " - "  + J.results[x].title + " - " + "["+J.results[x].release.catalogId+"]",  J.results[x].artistsTitle, J.results[x].release.title));
                        pb_dl.Value = i * limitNumber + x;
                    }
                }

            }
            URLString = null;
            J = null;
            pb_dl.Value = pb_dl.Maximum;
            GC.Collect();
            dl_thread = new Thread(new ThreadStart(queueDownloader));
            dl_thread.Start();
        }

        private void btn_debug_Click(object sender, EventArgs e)
        {
            if(dl_thread != null)
            {
                dl_thread.Abort();
            }
                
        }


        public void queueDownloader()
        {
            writeDebugLine("fnc: "+System.Reflection.MethodBase.GetCurrentMethod().Name);
            wc.Headers.Add(HttpRequestHeader.Cookie, "connect.sid=" + box_cookie.Text);
            string fn_string_clean ="";
            string dl_dir = @"download\";
            string pathstring = @"download\";
            string albumstring = "";
            pbar_updater(dq.queue.Count, 0);
            string completestring;
            Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");
            for (int i = 0; i < dq.queue.Count -1; i++)
            {
                albumstring = dq.queue[i].getan();
                pathstring = Path.Combine(Path.Combine(dl_dir,dq.queue[i].getartist()), albumstring);
                fn_string_clean = illegalInFileName.Replace(dq.queue[i].getfn(), "-");
                writeDebugFile("cleaned string:  " + fn_string_clean);
                completestring = Path.Combine(Path.Combine(prgPath, pathstring), fn_string_clean) +"."+extension;
                System.IO.Directory.CreateDirectory(Path.Combine(prgPath,pathstring));
                writeDebugFile("queueDownloader: in for loop url: " + dq.queue[i].geturl() + "    | "+ "filename : " + completestring);
                pbar_updater(dq.queue.Count, i);

                if (File.Exists(completestring))
                {
                    writeDebugFile("skipping URL: " + dq.queue[i].geturl() + " file already exists");
                }
                else
                {
                 try
                    {
                        
                        writeDebugFile("queueDownloader: downloading to: " + completestring);
                        try { update_dl_title(dq.queue[i].getfn()); } catch { }
                        using (WebClient fdwc= new WebClient())
                        {
                            fdwc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                            fdwc.Headers.Add(HttpRequestHeader.Cookie, "connect.sid=" + box_cookie.Text);
                            fdwc.DownloadFile(dq.queue[i].geturl(), completestring);
                        }

                    }
                    catch (Exception e)
                    {
                        writeDebugFile(e.ToString());
                    }
                }

            }
        }

        private void chck_podcast_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_podcast.Checked) { dl_podcast = true; }
        }




        public class DownloadObject
        {
            public DownloadObject(string _url, string _filename, string _artist, string _albumname)
            {
                download_url = _url;
                filename = _filename;
                artist = _artist;
                albumname = _albumname;
            }
            
            public string geturl()
            {
                return download_url;

            }
            public string getfn()
            {
                return filename;
            }
            public string getartist()
            {
                return artist;
            }
            public string getan()
            {
                return albumname;
            }
            public void seturl(string url)
            {
                download_url = url;
            }
            string artist;
            string download_url;
            string filename;
            string albumname;

        }
        public class DownloadQueue
        {
            public DownloadQueue()
            {
                queue = new List<DownloadObject>();
            }
            public void appendToQueue(DownloadObject d)
            {

                queue.Add(d);
            }
            public List<DownloadObject> queue;

        }
        public class Albums
        {
            public int trackNumber { get; set; }
            public string albumId { get; set; }
            public string _id { get; set; }
            public bool isFree { get; set; }
            public string streamHash { get; set; }
        }
        public class Artist
        {
            public string artistId { get; set; }
            public string name { get; set; }
            public string _id { get; set; }
        }
        public class Release
        {
            public string _id { get; set; }
            public string title { get; set; }
            public string renderedArtists { get; set; }
            public string type { get; set; }
            public string upc { get; set; }
            public string catalogId { get; set; }
            public string releaseDate { get; set; }
            public object preReleaseDate { get; set; }
            public string label { get; set; }
            public bool deleted { get; set; }
            public List<object> tags { get; set; }
            public bool freeDownloadForUsers { get; set; }
            public bool showToAdminsOnly { get; set; }
            public bool showOnWebsite { get; set; }
            public bool showAsFree { get; set; }
            public List<string> urls { get; set; }
            public string coverUrl { get; set; }
        }
        public class Result
        {
            public string _id { get; set; }
            public string title { get; set; }
            public string artistsTitle { get; set; }
            public string created { get; set; }
            public string label { get; set; }
            public bool deleted { get; set; }
            public bool hasErrors { get; set; }
            public List<string> catalogs { get; set; }
            public List<object> tags { get; set; }
            public bool licensable { get; set; }
            public List<object> genres { get; set; }
            public Albums albums { get; set; }
            public List<object> remixers { get; set; }
            public List<object> featuring { get; set; }
            public List<Artist> artists { get; set; }
            public string isrc { get; set; }
            public float? bpm { get; set; }
            public double duration { get; set; }
            public Release release { get; set; }
            public bool streamable { get; set; }
            public bool downloadable { get; set; }
            public bool inEarlyAccess { get; set; }
            public bool freeDownloadForUsers { get; set; }
        }
        public class MCatJSON
        {

            public int total { get; set; }
            public int skip { get; set; }
            public int limit { get; set; }
            public List<Result> results { get; set; }
        }

        private void chck_mixes_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_mixes.Checked) { dl_mixes = true; }
        }

        private void box_cookie_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
