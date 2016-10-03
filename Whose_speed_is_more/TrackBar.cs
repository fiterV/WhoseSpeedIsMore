using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Whose_speed_is_more
{
    class TrackBar
    {
        public int width;
        public Point location;
        public int status;
        public int max_status;
        public int one_move;
        public Color color;
        

        public TrackBar(Point LOCATION, int WIDTH, int STATUS, int MAX_STATUS, int ONE_MOVE, Color COLOR)
        {
            location = LOCATION;
            width = WIDTH;
            status = STATUS;
            max_status = MAX_STATUS;
            one_move = ONE_MOVE;
            color = COLOR;
        }

        public void Draw(Graphics gr, int delta)
        {
            SolidBrush p = new SolidBrush(color);
            gr.FillRectangle(new SolidBrush(color), new Rectangle(location.X, location.Y-status-delta, width, delta));
            status = Math.Min(max_status, status + delta);
        }

        public void Draw(Graphics gr)
        {
            gr.FillRectangle(new SolidBrush(color), new Rectangle(location.X, location.Y - status, width, status));
        }

    }
}
