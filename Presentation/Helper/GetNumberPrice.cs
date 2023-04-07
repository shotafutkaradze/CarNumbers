using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Helper
{
    public class GetNumberPrice
    {
        public static int CalPrice(string number)
        {
           

            var charcount =0;
            var price =0;

            var distinctchar = number.Where(char.IsLetter).Distinct();
            var digit = String.Join(string.Empty, number.Where(char.IsDigit).ToList());
            var numChar = String.Join(string.Empty, number.Where(char.IsLetter));

            var foursome = numChar.GroupBy(x => x).Where(x => x.Count() == 4).ToList();
            var threesome = numChar.GroupBy(x => x).Where(x => x.Count() == 3).ToList();
            var nosome = numChar.GroupBy(x => x).Where(x => x.Count() == 0).ToList();

            if(foursome.Count != 0) { 
                if(ContainsNumber(digit) == 1){ return price = 1000; }
                if(ContainsNumber(digit) == 2){ return price = 800; }
                if(ContainsNumber(digit) == 3){ return price = 600; }
            }   
            if (threesome.Count != 0 || nosome.Count == 0)
            {
                if (ContainsNumber(digit) == 1) { return price = 800; }
                if (ContainsNumber(digit) == 2) { return price = 600; }
                if (ContainsNumber(digit) == 3) { return price = 400; }
            }


            int ContainsNumber(string num)
            {
                var numbers = new[] { "111", "222", "333", "444", "555", "666", "777", "888", "999" };
                var numbers1 = new[] { "001", "002", "003", "004", "005", "006", "007", "008", "009" };
                var numbers2 = new[] { "100", "200", "300", "400", "500", "600", "700", "800", "900" };

                if (numbers.Contains(num)) { return 1; }
                if (numbers1.Contains(num)) { return 2; }
                if (numbers2.Contains(num)) { return 3; }
                return 0;
            }
            
            return price;
        }

       
    }
}
