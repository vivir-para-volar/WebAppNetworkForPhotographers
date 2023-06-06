namespace EmployeeClientAppNetworkForPhotographers.Models
{
    public class ViewSelectList
    {
        public string? ValueStr { get; set; }
        public int? ValueInt { get; set; }
        public string Name { get; set; }

        public ViewSelectList(string value, string name)
        {
            ValueStr = value;
            Name = name;
        }

        public ViewSelectList(int value, string name)
        {
            ValueInt = value;
            Name = name;
        }
    }
}
