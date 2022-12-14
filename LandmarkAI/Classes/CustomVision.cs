using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandmarkAI.Classes
{
    public class Prediction
    {
        
        public string tagId { get; set; }
        public string tagName { get; set; }
        public double probability { get; set; }
    }

    public class CustomVision
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
        public DateTime Created { get; set; }
        public List<Prediction> Predictions { get; set; }
    }

}
