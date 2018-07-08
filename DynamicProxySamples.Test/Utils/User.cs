using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProxySamples.Test.Utils
{
    public class User
    {
        public virtual string UserName { get; set; }
        public virtual void Greet()
        {
            
        }
        public virtual void SayGoodbye()
        {
            
        }

        public virtual void Greet(string greet)
        {

        }

      
    }
}
