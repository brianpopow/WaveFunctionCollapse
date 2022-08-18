using WaveFunctionCollapseModel.Data;

namespace WaveFunctionCollapseModel.Extensions
{
    internal static class SymmetryTypeExtensions
    {
        public static SymmetryType ToSymmetryType(this char c)
        {
            switch (c)
            {
                case 'L':
                    return SymmetryType.L;
                case 'T':
                    return SymmetryType.T;
                case 'I':
                    return SymmetryType.I;
                case '\\':
                    return SymmetryType.Backslash;
                case 'F':
                    return SymmetryType.F;
                default:
                    return SymmetryType.X;
            }
        }
    }
}
