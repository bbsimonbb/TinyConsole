using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;
using System.Reflection;
using System.Configuration;

namespace TinyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctr = TinyIoCContainer.Current;
            var assemblies = new List<Assembly>();
            var helperAssemblyPath = ConfigurationManager.AppSettings["HelperAssembly"];
            // There may or may not be a helper assembly, which might or might not override some of our implementations...
            // Add it first in the list of assemblies to autoregister, and first in wins...
            if (!string.IsNullOrEmpty(helperAssemblyPath))
                assemblies.Add(Assembly.LoadFrom(helperAssemblyPath));
            assemblies.Add(Assembly.GetExecutingAssembly());
            ctr.AutoRegister(assemblies);
            var smoothDefault = ctr.Resolve<ISmoothInterface>();
            Console.WriteLine(smoothDefault.Greet());
            var smoothEn = ctr.Resolve<ISmoothInterface>("En");
            Console.WriteLine(smoothEn.Greet());
            var smoothFr = ctr.Resolve<ISmoothInterface>("Fr");
            Console.WriteLine(smoothFr.Greet());
            Console.ReadLine();
        }
        
    }
    public interface ISmoothInterface
    {
        string Greet();
    }
    public class SimonSingle : ISmoothInterface
    {
        public string Greet()
        {
            return "By default, I like you.";
        }
    }
    public class SimonSingleDupl : ISmoothInterface
    {
        public string Greet()
        {
            return "Duplicate should be ignored.";
        }
    }

    [RegistrationName("En")]
    public class SmoothEn : ISmoothInterface
    {
        public string Greet()
        {
            return "Do you come here often?";
        }
    }
    [RegistrationName("En")]
    public class SmoothDupl : ISmoothInterface
    {
        public string Greet()
        {
            return "This is a duplicate and should not register.";
        }
    }
    [RegistrationName("Fr")]
    public class SmoothFr : ISmoothInterface
    {
        public string Greet()
        {
            return "Tu me plait, tu sais?";
        }
    }
}
