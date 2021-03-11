using System;

namespace BoardGameApiTests.Models
{
    public class SetupSummary
    {
        public bool IsOkay { get; set; }
        public string Description { get; set; }
        public Guid SessionId { get; set; }
    }
}