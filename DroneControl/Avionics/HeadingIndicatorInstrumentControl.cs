/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : HeadingIndicatorInstrumentControl.cs                           */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The heading indicator instrument control                       */
/* Creation : 25/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Data;
using DroneControl.Avionics;

namespace DroneControl.Avionics
{
    class HeadingIndicatorInstrumentControl : InstrumentControl
    {
        #region Fields


        private bool rotateAircraft = false;
        public bool RotateAircraft
        {
            get { return rotateAircraft; }
            set
            {
                if (value != rotateAircraft)
                {
                    rotateAircraft = value;
                    Refresh();
                }
            }
        }

        // Parameters
        int Heading = 30; 

        // Images
        Bitmap bmpCadran = new Bitmap(AvionicsInstrumentsControlsRessources.HeadingIndicator_Background);
        Bitmap bmpHedingWeel = new Bitmap(AvionicsInstrumentsControlsRessources.HeadingWeel);
        Bitmap bmpAircaft = new Bitmap(AvionicsInstrumentsControlsRessources.HeadingIndicator_Aircraft);        

        #endregion

        #region Contructor

        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public HeadingIndicatorInstrumentControl()
		{
			// Double bufferisation
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint, true);
        }

        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);

            // Pre Display computings
            Point ptRotation = new Point(150, 150);
            Point ptImgAircraft = new Point(73,41);
            Point ptImgHeadingWeel = new Point(13, 13);

            bmpCadran.MakeTransparent(Color.Yellow);
            bmpHedingWeel.MakeTransparent(Color.Yellow);
            bmpAircaft.MakeTransparent(Color.Yellow);

            double alphaHeadingWeel = InterpolPhyToAngle(Heading,0,360,360,0);

            float scale = (float)this.Width / bmpCadran.Width;

            // diplay mask
            Pen maskPen = new Pen(this.BackColor, 30 * scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, bmpCadran.Width * scale, bmpCadran.Height * scale);

            // display cadran
            pe.Graphics.DrawImage(bmpCadran, 0, 0, (float)(bmpCadran.Width * scale), (float)(bmpCadran.Height * scale));

            // display HeadingWeel
            if (RotateAircraft)
                pe.Graphics.DrawImage(bmpHedingWeel, (int)(ptImgHeadingWeel.X * scale), (int)(ptImgHeadingWeel.Y * scale), (float)(bmpHedingWeel.Width * scale), (float)(bmpHedingWeel.Height * scale));
            else
                RotateImage(pe,bmpHedingWeel, alphaHeadingWeel, ptImgHeadingWeel, ptRotation, scale);

            // display aircraft
            if (RotateAircraft)
                RotateImage(pe, bmpAircaft, -alphaHeadingWeel, ptImgAircraft, ptRotation, scale);
            else
                pe.Graphics.DrawImage(bmpAircaft, (int)(ptImgAircraft.X*scale), (int)(ptImgAircraft.Y*scale), (float)(bmpAircaft.Width * scale), (float)(bmpAircaft.Height * scale));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Define the physical value to be displayed on the indicator
        /// </summary>
        /// <param name="aircraftHeading">The aircraft heading in �deg</param>
        public void SetHeadingIndicatorParameters(int aircraftHeading)
        {
            Heading = aircraftHeading;

            this.Refresh();
        }

        #endregion
    }
}
