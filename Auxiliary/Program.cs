using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FlickrSync
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static FlickrSync MainFlickrSync;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainFlickrSync = new FlickrSync();

            if (MainFlickrSync != null)
            {
                if (args.Length > 0)
                {
                    foreach (string str in args)
                    {
                        if (str.ToLower().Contains(@"/auto"))
                            FlickrSync.autorun = true;
                    }
                }

                try
                {
                    Application.Run(MainFlickrSync);
                }
                catch (Exception ex)
                {
                    FlickrSync.Error("Unknown error detected - exiting FlickrSync.", ex, FlickrSync.ErrorType.FatalError);
                }
            }
        }
    }
}