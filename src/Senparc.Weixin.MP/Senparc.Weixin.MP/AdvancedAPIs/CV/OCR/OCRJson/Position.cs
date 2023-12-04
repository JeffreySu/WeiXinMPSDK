
namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    public class Pos_Location
    {
        /// <summary>
        /// 
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int y { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Pos
    {
        /// <summary>
        /// 
        /// </summary>
        public Pos_Location left_top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Pos_Location right_top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Pos_Location right_bottom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Pos_Location left_bottom { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 
        /// </summary>
        public Pos pos { get; set; }
    }
}
