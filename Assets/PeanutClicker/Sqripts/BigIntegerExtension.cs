using System.Numerics; // BigInteger用
using System.Collections.Generic;

namespace Assets.PeanutClicker.Sqripts
{
    /// <summary>
    /// BigInteger型を拡張して、日本語の単位（万・億・兆…）を表示できるようにするクラス
    /// </summary>
    public static class BigIntegerExtension // 拡張メソッドを定義する場合はstaticクラスにする
    {
        // 単位の配列（無量大数まで対応）
        private static readonly string[] Units = new string[]
        {
        "", "万", "億", "兆", "京", "垓", "𥝱", "穣", "溝", "澗", "正", "載", "極", "恒河沙", "阿僧祇", "那由他", "不可思議", "無量大数"
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToJpString(this BigInteger value, string format = "F2")
        {
            return value.ToString();
        }
    }
}
