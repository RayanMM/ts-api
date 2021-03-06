using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS_Security
{
    public class Encriptor : ISecurity
    {
        private readonly IConfiguration configuration; 

        public Encriptor(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

		public virtual string Encode(string str)
		{
			string chaveId = configuration["auth:ENCRIPTOR_KEY"];
			int nLength = 0;
			string strHash = "";
			int cCurr = 0;
			int i = 0;
			int cod = 0;
			int chv = 0;
			int temp = 0;
			nLength = str.Length;

			for (i = 0; i < nLength; i++)
			{
				//converte COD string em byte ascii
				byte[] b_cod = new byte[Encoding.ASCII.GetByteCount(str[i].ToString())];
				b_cod = Encoding.Unicode.GetBytes(str[i].ToString());

				//converte CHV string em byte ascii
				byte[] b_chv = new byte[Encoding.ASCII.GetByteCount(chaveId[i].ToString())];
				b_chv = Encoding.Unicode.GetBytes(chaveId[i].ToString());


				cod = b_cod[0];
				chv = b_chv[0];


				if (cCurr >= chaveId.Length - 1)
				{
					cCurr = 0;
				}
				else
				{
					cCurr++;
				}
				if (i % 2 == 0)
				{
					if (cod + chv > 255)
					{
						temp = ((cod + chv) % 256);
					}
					else
					{
						temp = cod + chv;
					}
					strHash = strHash + Charact(temp);
				}
				else
				{
					if (cod - chv < 0)
					{
						temp = ((cod - chv) % 256) + 256;
					}
					else
					{
						temp = cod - chv;
					}
					strHash = strHash + Charact(temp);
				}
			}
			return Base64Encode(strHash);
		}
		public virtual string Decode(string str)
		{
			string chaveId = configuration["auth:ENCRIPTOR_KEY"];
			string strNormal = "";
			str = Base64Decode(str);

			int cCurr = 0;
			int i = 0;
			int cod = 0;
			int chv = 0;
			int temp = 0;

			for (i = 0; i < str.Length; i++)
			{

				byte[] b_cod = new byte[Encoding.ASCII.GetByteCount(str[i].ToString())];
				b_cod = Encoding.Unicode.GetBytes(str[i].ToString());

				byte[] b_chv = new byte[Encoding.ASCII.GetByteCount(chaveId[i].ToString())];
				b_chv = Encoding.Unicode.GetBytes(chaveId[i].ToString());

				cod = b_cod[0];
				chv = b_chv[0];

				if (cCurr >= chaveId.Length)
				{
					cCurr = 0;
				}
				else
				{
					cCurr++;
				}
				if (i % 2 == 0)
				{
					if (cod - chv < 0)
					{
						temp = ((cod - chv) % 256) + 256;
					}
					else
					{
						temp = cod - chv;
					}
					strNormal = strNormal + Charact(temp);
				}
				else
				{
					if (cod + chv > 255)
					{
						temp = ((cod + chv) % 256);
					}
					else
					{
						temp = cod + chv;
					}
					strNormal = strNormal + Charact(temp);
				}
			}
			return strNormal;
		}
		private string Base64Encode(string plainText)
		{
			Encoding iso = Encoding.GetEncoding(28591);
			var plainTextBytes = iso.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}
		private string Base64Decode(string base64EncodedData)
		{
			Encoding iso = Encoding.GetEncoding(28591);
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return iso.GetString(base64EncodedBytes);
		}
		private string Charact(int var)
		{
			if (var < 935)
			{
				return Char.ConvertFromUtf32(var);
			}
			else if (var >= 35 && var <= 255)
			{
				string[] chars = new string[256];
				chars[35] = @"#";
				chars[36] = @"$";
				chars[37] = @"%";
				chars[38] = @"&";
				chars[39] = @"'";
				chars[40] = @"(";
				chars[41] = @")";
				chars[42] = @"*";
				chars[43] = @"+";
				chars[44] = @",";
				chars[45] = @"-";
				chars[46] = @".";
				chars[47] = @"/";
				chars[48] = @"0";
				chars[49] = @"1";
				chars[50] = @"2";
				chars[51] = @"3";
				chars[52] = @"4";
				chars[53] = @"5";
				chars[54] = @"6";
				chars[55] = @"7";
				chars[56] = @"8";
				chars[57] = @"9";
				chars[58] = @":";
				chars[59] = @";";
				chars[60] = @"<";
				chars[61] = @"=";
				chars[62] = @">";
				chars[63] = @"?";
				chars[64] = @"@";
				chars[65] = @"A";
				chars[66] = @"B";
				chars[67] = @"C";
				chars[68] = @"D";
				chars[69] = @"E";
				chars[70] = @"F";
				chars[71] = @"G";
				chars[72] = @"H";
				chars[73] = @"I";
				chars[74] = @"J";
				chars[75] = @"K";
				chars[76] = @"L";
				chars[77] = @"M";
				chars[78] = @"N";
				chars[79] = @"O";
				chars[80] = @"P";
				chars[81] = @"Q";
				chars[82] = @"R";
				chars[83] = @"S";
				chars[84] = @"T";
				chars[85] = @"U";
				chars[86] = @"V";
				chars[87] = @"W";
				chars[88] = @"X";
				chars[89] = @"Y";
				chars[90] = @"Z";
				chars[91] = @"[";
				chars[92] = @"\";
				chars[93] = @"]";
				chars[94] = @"^";
				chars[95] = @"_";
				chars[96] = @"`";
				chars[97] = @"a";
				chars[98] = @"b";
				chars[99] = @"c";
				chars[100] = @"d";
				chars[101] = @"e";
				chars[102] = @"f";
				chars[103] = @"g";
				chars[104] = @"h";
				chars[105] = @"i";
				chars[106] = @"j";
				chars[107] = @"k";
				chars[108] = @"l";
				chars[109] = @"m";
				chars[110] = @"n";
				chars[111] = @"o";
				chars[112] = @"p";
				chars[113] = @"q";
				chars[114] = @"r";
				chars[115] = @"s";
				chars[116] = @"t";
				chars[117] = @"u";
				chars[118] = @"v";
				chars[119] = @"w";
				chars[120] = @"x";
				chars[121] = @"y";
				chars[122] = @"z";
				chars[123] = @"{";
				chars[124] = @"|";
				chars[125] = @"}";
				chars[126] = @"~";
				chars[127] = @"[]";
				chars[128] = @"€";
				chars[129] = @"";
				chars[130] = @"‚";
				chars[131] = @"ƒ";
				chars[132] = @"„";
				chars[133] = @"…";
				chars[134] = @"†";
				chars[135] = @"‡";
				chars[136] = @"ˆ";
				chars[137] = @"‰";
				chars[138] = @"Š";
				chars[139] = @"‹";
				chars[140] = @"Œ";
				chars[141] = @"";
				chars[142] = @"Ž";
				chars[143] = @"";
				chars[144] = @"";
				chars[145] = @"‘";
				chars[146] = @"’";
				chars[147] = @"“";
				chars[148] = @"”";
				chars[149] = @"•";
				chars[150] = @"–";
				chars[151] = @"—";
				chars[152] = @"˜";
				chars[153] = @"™";
				chars[154] = @"š";
				chars[155] = @"›";
				chars[156] = @"œ";
				chars[157] = @"";
				chars[158] = @"ž";
				chars[159] = @"Ÿ";
				chars[160] = @"0";
				chars[161] = @"¡";
				chars[162] = @"¢";
				chars[163] = @"£";
				chars[164] = @"¤";
				chars[165] = @"¥";
				chars[166] = @"¦";
				chars[167] = @"§";
				chars[168] = @"¨";
				chars[169] = @"©";
				chars[170] = @"ª";
				chars[171] = @"«";
				chars[172] = @"¬";
				chars[173] = @"­";
				chars[174] = @"®";
				chars[175] = @"¯";
				chars[176] = @"°";
				chars[177] = @"±";
				chars[178] = @"²";
				chars[179] = @"³";
				chars[180] = @"´";
				chars[181] = @"µ";
				chars[182] = @"¶";
				chars[183] = @"·";
				chars[184] = @"¸";
				chars[185] = @"¹";
				chars[186] = @"º";
				chars[187] = @"»";
				chars[188] = @"¼";
				chars[189] = @"½";
				chars[190] = @"¾";
				chars[191] = @"¿";
				chars[192] = @"À";
				chars[193] = @"Á";
				chars[194] = @"Â";
				chars[195] = @"Ã";
				chars[196] = @"Ä";
				chars[197] = @"Å";
				chars[198] = @"Æ";
				chars[199] = @"Ç";
				chars[200] = @"È";
				chars[201] = @"É";
				chars[202] = @"Ê";
				chars[203] = @"Ë";
				chars[204] = @"Ì";
				chars[205] = @"Í";
				chars[206] = @"Î";
				chars[207] = @"Ï";
				chars[208] = @"Ð";
				chars[209] = @"Ñ";
				chars[210] = @"Ò";
				chars[211] = @"Ó";
				chars[212] = @"Ô";
				chars[213] = @"Õ";
				chars[214] = @"Ö";
				chars[215] = @"×";
				chars[216] = @"Ø";
				chars[217] = @"Ù";
				chars[218] = @"Ú";
				chars[219] = @"Û";
				chars[220] = @"Ü";
				chars[221] = @"Ý";
				chars[222] = @"Þ";
				chars[223] = @"ß";
				chars[224] = @"à";
				chars[225] = @"á";
				chars[226] = @"â";
				chars[227] = @"ã";
				chars[228] = @"ä";
				chars[229] = @"å";
				chars[230] = @"æ";
				chars[231] = @"ç";
				chars[232] = @"è";
				chars[233] = @"é";
				chars[234] = @"ê";
				chars[235] = @"ë";
				chars[236] = @"ì";
				chars[237] = @"í";
				chars[238] = @"î";
				chars[239] = @"ï";
				chars[240] = @"ð";
				chars[241] = @"ñ";
				chars[242] = @"ò";
				chars[243] = @"ó";
				chars[244] = @"ô";
				chars[245] = @"õ";
				chars[246] = @"ö";
				chars[247] = @"÷";
				chars[248] = @"ø";
				chars[249] = @"ù";
				chars[250] = @"ú";
				chars[251] = @"û";
				chars[252] = @"ü";
				chars[253] = @"ý";
				chars[254] = @"þ";
				chars[255] = @"ÿ";

				return chars[var];

			}
			return "<" + var + ">";
		}
	}
}


