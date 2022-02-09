namespace Aquilion.Notepad.Core
{
    public class Node
    {
        public Node(string path, string pathname)
        {
            Path = path;
            PathName = pathname;
        }

        public Node PreviousNode { get; set; }
        public Node NextNode { get; set; }
        public string Path { get; set; }
        public string PathName { get; set; }
    }
}
