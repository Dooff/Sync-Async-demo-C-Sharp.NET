using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sync_Async_Demo
{
    public static class Methods
    {
        public static List<WebsiteDataModel> RunDownloadSync()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();

            websites.ForEach(site =>
            {
                WebsiteDataModel results = DownloadWebsite(site);
                output.Add(results);
            });
            return output;
        }
        public static List<WebsiteDataModel> RunDownloadParallelSync()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();

            Parallel.ForEach<string>(websites, (site) =>
            {
                WebsiteDataModel results = DownloadWebsite(site);
                output.Add(results);
            });
            return output;
        }
        public async static Task<List<WebsiteDataModel>> RunDownloadParallelAsyncV2(IProgress<ProgressReportModel> progress)//, CancellationToken cancellationToken)
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            ProgressReportModel report = new ProgressReportModel();
            await Task.Run(() =>
            {
                Parallel.ForEach<string>(websites, (site) =>
                {
                    WebsiteDataModel results = DownloadWebsite(site);
                    output.Add(results);
                    //cancellationToken.ThrowIfCancellationRequested();
                    report.SitesDownloaded = output;
                    report.PercentageComplete = (output.Count * 100) / websites.Count;
                    progress.Report(report);
                });
            });
            return output;
        }

        public static async Task<List<WebsiteDataModel>> RunDownloadAsync(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken)
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            foreach (string site in websites)
            {
                WebsiteDataModel results = await DownloadWebsiteAsync(site);
                output.Add(results);

                cancellationToken.ThrowIfCancellationRequested();

                report.SitesDownloaded = output;
                report.PercentageComplete = (output.Count * 100) / websites.Count;
                progress.Report(report);
            }
            return output;
        }

        public static async Task<List<WebsiteDataModel>> RunDownloadParallelAsync()
        {
            List<string> websites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (string site in websites)
                tasks.Add(DownloadWebsiteAsync(site));

            //tasks.Add(Task.Run(() => DownloadWebsite(site)));

            var results = await Task.WhenAll(tasks);

            return new List<WebsiteDataModel>(results);
        }

        private static List<string> PrepData()
        {
            List<string> output = new List<string>() { "https://www.yahoo.com", "https://www.google.com",
                                                       "https://www.microsoft.com", "https://www.github.com",
                                                       "https://www.youtube.com", "https://www3.animeflv.net/anime/hunter-x-hunter-2011",
                                                       "https://www.codeproject.com", "https://www.stackoverflow.com",
                                                        "https://www.amazon.com", "https://www.twitter.com", "https://www.instagram.com"};
            
            return output;

        }

        private static WebsiteDataModel DownloadWebsite(string websiteUrl)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebSiteUrl = websiteUrl;
            output.WebSiteData = client.DownloadString(websiteUrl);

            return output;
        }
        private static async Task<WebsiteDataModel> DownloadWebsiteAsync(string websiteUrl)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebSiteUrl = websiteUrl;
            output.WebSiteData = await client.DownloadStringTaskAsync(websiteUrl);

            return output;
        }
    }
}
