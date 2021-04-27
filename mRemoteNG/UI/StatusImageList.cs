using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using mRemoteNG.App;
using mRemoteNG.Connection;
using mRemoteNG.Container;
using mRemoteNG.Tree.Root;

namespace mRemoteNG.UI
{
    public class StatusImageList : IDisposable
    {
        public ImageList ImageList { get; }

        public StatusImageList()
        {
            var display = new DisplayProperties();

            // shahid customization start: need to set bigger horizon size so to adjust all that i require

            ImageList = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(
                                     (int)Math.Round(16 * display.ResolutionScalingFactor.Width),
                                     (int)Math.Round(16 * display.ResolutionScalingFactor.Height)),
                TransparentColor = Color.Transparent
            };

            FillImageList(ImageList);
        }

        public object ImageGetter(object rowObject)
        {
            return GetKey(rowObject as ConnectionInfo);
        }

        public Image GetImage(ConnectionInfo connectionInfo)
        {
            var key = GetKey(connectionInfo);
            return ImageList.Images.ContainsKey(key)
                ? ImageList.Images[key]
                : null;
        }

        public string GetKey(ConnectionInfo connectionInfo)
        {
            if (connectionInfo == null) return "";
            if (connectionInfo is RootPuttySessionsNodeInfo) return "PuttySessions";
            if (connectionInfo is RootNodeInfo) return "Root";
            if (connectionInfo is ContainerInfo) return "Folder";

            return GetConnectionIcon_v2(connectionInfo);
        }

        private static string BuildConnectionIconName(string icon, bool connected)
        {

            var status = connected ? "Play" : "Default";
            return $"Connection_{icon}_{status}";
        }

        private static string GetConnectionIconName(string icon, ConnectionInfo connectionInfo)
        {
            string name = null;

            switch (connectionInfo.ConnectionCurrentState)
            {
                case ConnectionInfo.ConnectionState.Connected:  name = $"Connection_{icon}_Play";
                    break;
                case ConnectionInfo.ConnectionState.NotConnected: name = $"Connection_{icon}_Default";
                    break;
                case ConnectionInfo.ConnectionState.BusySaving:
                    break;
                case ConnectionInfo.ConnectionState.BusyRestoring:
                    break;
                case ConnectionInfo.ConnectionState.Connecting:  name = $"{ connectionInfo.GetLastWaitImageIndex(36).ToString() }";
                    break;
                default:
                    break;
            }

            return name;
        }


        private const string DefaultConnectionIcon = "";

        private string GetConnectionIcon(ConnectionInfo connection)
        {
            if (string.IsNullOrEmpty(connection.Icon))
            {
                return DefaultConnectionIcon;
            }

            var connected = connection.OpenConnections.Count > 0;
            var name = BuildConnectionIconName(connection.Icon, connected);
            if (ImageList.Images.ContainsKey(name)) return name;
            var image = ConnectionIcon.FromString(connection.Icon);
            if (image == null)
            {
                return DefaultConnectionIcon;
            }

            ImageList.Images.Add(BuildConnectionIconName(connection.Icon, false), image);
            ImageList.Images.Add(BuildConnectionIconName(connection.Icon, true),
                                 Overlay(image, Properties.Resources.ConnectedOverlay));
            return name;
        }

        // -------------------------------------------------------------------------------------- Shahid Change: i need complex icon rendering based on new logic and animations feature
        private string GetConnectionIcon_v2(ConnectionInfo connectionInfo)
        {

            if (string.IsNullOrEmpty(connectionInfo.Icon))   // If no Icon mentioned or present there to use as  base
            {
                return DefaultConnectionIcon;
            }

            var connected = connectionInfo.OpenConnections.Count > 0;
            var name = GetConnectionIconName(connectionInfo.Icon, connectionInfo);

            if (ImageList.Images.ContainsKey(name)) return name;  // if List is populated already then just select and return.


            // ------------------------------------------- Populate Image List ---------------------------------------------------


            var image = ConnectionIcon.FromString(connectionInfo.Icon);
            if (image == null)
            {
                return DefaultConnectionIcon;    // if no icon is selected then nothing to work here 
            }

            ImageList.Images.Add(BuildConnectionIconName(connectionInfo.Icon, false), image);
            ImageList.Images.Add(BuildConnectionIconName(connectionInfo.Icon, true),
                                 Overlay(image, Properties.Resources.ConnectedOverlay));

            MakeFrames(ImageList, 10);

            return name;
        }

        private static Bitmap Overlay(Icon background, Image foreground)
        {
            var result = new Bitmap(background.ToBitmap(), new Size(16, 16));
            using (var gr = Graphics.FromImage(result))
            {
                gr.DrawImage(foreground, new Rectangle(0, 0, foreground.Width, foreground.Height));
            }

            return result;
        }

        

        private static void FillImageList(ImageList imageList)
        {
            try
            {
                imageList.Images.Add("Root", Properties.Resources.Root);
                imageList.Images.Add("Folder", Properties.Resources.Folder);
                imageList.Images.Add("PuttySessions", Properties.Resources.PuttySessions);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionStackTrace(
                                                                $"Unable to fill the image list of type {nameof(StatusImageList)}",
                                                                ex);
            }
        }

        private void MakeFrames(ImageList imageList1, float angle)
        {
            Bitmap ico = Properties.Resources.loading_icon_black.ToBitmap();
            int index = 0;

            for (float i = 0; i < 360; i += angle)
            {
                Image rotated = RotateImage(ico, i);

                imageList1.Images.Add($"{index.ToString()}", rotated);

                index++;
            }
        }

        #region ImageHelperFuncs

        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ImageList?.Dispose();
            }
        }
    }
}