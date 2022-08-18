namespace WaveFunctionCollapseModel.Data
{
    internal class TileData
    {
        public TileData(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public List<string> Images { get; set; }

        public int Cardinality { get; set; }

        public char Symmetry { get; set; }
    }
}
