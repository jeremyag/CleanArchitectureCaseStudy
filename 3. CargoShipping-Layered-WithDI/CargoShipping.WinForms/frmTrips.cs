using CargoShipping.Repository;
using CargoShipping.Service;
using CargoShipping.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargoShipping.WinForms
{
    public partial class frmTrips : Form
    {
        private readonly ITripSegmentService tripSegmentService;

        public frmTrips(ITripSegmentService tripSegmentService)
        {
            InitializeComponent();

            this.tripSegmentService = tripSegmentService;
        }

        private void btnSearchByTripNumber_Click(object sender, EventArgs e)
        {
            var segments = tripSegmentService.GetTripSegmentsByTripNumber(txtTripNumber.Text);            
            gvTrips.DataSource = segments;
        }        

        private void gvTrips_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!gvTrips.Columns.Contains("Warning"))
            {
                var warningColumn = new DataGridViewButtonColumn
                {
                    Text = "Warning",
                    Name = "Warning",
                    Width = 140
                };
                gvTrips.Columns.Add(warningColumn);
            }
            if (gvTrips.Columns.Contains("VarianceWarning"))
            {
                gvTrips.Columns.Remove("VarianceWarning");
            }
        }

        private void gvTrips_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //display warning sign if variance is greater than 10%
            var segments = gvTrips.DataSource as List<TripSegmentViewModel>;
            if (e.RowIndex < 0 || e.RowIndex >= segments.Count) return;            

            if (segments[e.RowIndex].VarianceWarning)
            {
                if (e.ColumnIndex >= 0 && gvTrips.Columns[e.ColumnIndex].Name == "Warning" && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);                    
                    e.Graphics.DrawImage(Resource.Warning, 
                        (int)((e.CellBounds.Width / 2) - (Resource.Warning.Width / 2)) + e.CellBounds.Left,
                        (int)((e.CellBounds.Height / 2) - (Resource.Warning.Height / 2)) + e.CellBounds.Top);
                    e.Handled = true;
                }
            }            
        }
    }
}
