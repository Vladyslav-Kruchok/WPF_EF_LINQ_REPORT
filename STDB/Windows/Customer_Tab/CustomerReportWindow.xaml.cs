using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Reporting.WinForms;
using System.Data.Entity;

namespace STDB
{
    /// <summary>
    /// Interaction logic for CustomerReportWindow.xaml
    /// </summary>
    public partial class CustomerReportWindow : Window
    {
        private bool _isReportViewerLoaded;
        private DbSet<EF_Model.Customer> full_view;
        private List<EF_Model.Customer> partial_view;

        public CustomerReportWindow()
        {
            InitializeComponent();
        }
        public CustomerReportWindow(DbSet<EF_Model.Customer> f_full_view)
        {
            InitializeComponent();
            partial_view = null;
            full_view = f_full_view;
            _reportViewer.Load += ReportViewer_Load;
        }
        public CustomerReportWindow(List<EF_Model.Customer> f_partial_view)
        {
            InitializeComponent();
            full_view = null;
            partial_view = f_partial_view;
            _reportViewer.Load += ReportViewer_Load;
        }
        private void ReportViewer_report<TType>(TType view)
        {
            if (!_isReportViewerLoaded)
            {
                ReportDataSource reportDSource = new ReportDataSource();
                reportDSource.Name = "CustomerDataSetNameEF";
                reportDSource.Value = view;
                _reportViewer.LocalReport.ReportEmbeddedResource = "STDB.Report.CustomerReport.rdlc";
                _reportViewer.LocalReport.DataSources.Add(reportDSource);
                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (full_view != null)
            {
                ReportViewer_report<DbSet<EF_Model.Customer>>(full_view);
            }
            if (partial_view != null)
            {
                ReportViewer_report<List<EF_Model.Customer>>(partial_view);
            }
        }
      }
    }
