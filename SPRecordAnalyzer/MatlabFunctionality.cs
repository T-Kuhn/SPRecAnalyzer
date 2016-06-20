using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPRecordAnalyzer
{
    public static class MatlabFunctionality
    {
        public static void buttonStartMatlab_Click(object sender, EventArgs e)
        {
            // Create the MATLAB instance 
            MLApp.MLApp matlab = new MLApp.MLApp();

            // Change to the directory where the function is located 
            matlab.Execute(@"cd d:\KT\Matlab");

            matlab.Execute(@"gcaEx");

            // Define the output
            //object result = null;

            // Call the MATLAB function myfunc
            //matlab.Feval("gcaEx", 0, out result);

            // Display result
            //object[] res = result as object[];
        }

    }
}
