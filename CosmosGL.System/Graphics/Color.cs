namespace CosmosGL.System.Graphics
{
    public class Color
    {
        //public byte R { get; set; }
        // public byte G { get; set; }
        // public byte B { get; set; }
        public byte A { get; set; }


        private byte _R;

        public byte R
        {
            get { return _R; }
            set
            {
                _R = value;
                _Hex = ((R << 16) | (G << 8) | B);
            }
        }

        private byte _G;

        public byte G
        {
            get { return _G; }
            set
            {
                _G = value;
                _Hex = ((R << 16) | (G << 8) | B);
            }
        }

        private byte _B;

        public byte B
        {
            get { return _B; }
            set
            {
                _B = value;
                _Hex = ((R << 16) | (G << 8) | B);
            }
        }


        private int _Hex { get; set; }

        public Color(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            A = 255;
        }

        public Color(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            A = a;
        }

        public Color(Color c, byte a)
        {
            this.R = c.R;
            this.G = c.G;
            this.B = c.B;
            A = a;
        }

        public Color(int hex)
        {
            R = ((byte) (hex >> 16));
            G = ((byte) (hex >> 8));
            B = ((byte) (hex >> 0));
            A = 255;
        }

        public Color()
        {
        }

        public int ToHex() => _Hex;

        public static implicit operator int(Color c)
        {
            return c.ToHex();
        }
    }
}