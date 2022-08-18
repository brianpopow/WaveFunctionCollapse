namespace WaveFunctionCollapseModel.Data
{
    public class TileData
    {
        public TileData(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public List<ImageData> Images { get; set; }

        public int Cardinality { get; set; }

        public SymmetryType Symmetry { get; set; }
    }
}
