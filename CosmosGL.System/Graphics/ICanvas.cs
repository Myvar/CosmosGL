namespace CosmosGL.System.Graphics
{
    public interface ICanvas
    {
        int Height { get; set; }
        int Width { get; set; }

        void Clear(uint c);
        Color GetPixel(int x, int y);
        void SetPixel(int x, int y, Color c);
        void SetPixel(int x, int y, uint c);
        void WriteToScreen();
    }
}