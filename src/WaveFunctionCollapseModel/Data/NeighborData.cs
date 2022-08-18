namespace WaveFunctionCollapseModel.Data
{
    internal class NeighborData
    {
        public NeighborData(string[] left, string[] right)
        {
            this.Left = left;
            this.Right = right;
        }

        public string[] Left { get; }

        public string[] Right { get; }
    }
}
