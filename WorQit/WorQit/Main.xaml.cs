using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WorQit
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class Main : Page
    {
        private List<Vacature> vacatureLijst { get; set; }
        //private ServiceReference1.Service1Client client = new Service1Client();

        public Main()
        {
            this.InitializeComponent();
        }
    }
}
