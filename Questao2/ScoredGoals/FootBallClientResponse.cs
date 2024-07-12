using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2.ScoredGoals
{
    public class FootBallClientResponse
    {
        public int Page { get; set; }
        public int Total_Pages { get; set; }
        public List<Data> ?Data { get; set; }
    }
}
