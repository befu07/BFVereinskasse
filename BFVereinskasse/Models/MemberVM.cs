using BFVereinskasse.Data;
using System.Runtime.CompilerServices;

namespace BFVereinskasse.Models
{
    public class MemberVM 
    {
        public int Id { get; set; } 
        public string? IsActiveString { get; set; }
        public bool GetStatus()
        {
            if (IsActiveString == "on")
                return true;
            return false;
        }
    }
}
