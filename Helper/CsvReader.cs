using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace BondVault.Helper
{
    public sealed class CsvReader : IDisposable
    {
        private bool __hasHeader;

        private long __rowno = 0;
        private TextReader __reader;
        private static Regex rexCsvSplitter = new Regex( @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))" );
        private static Regex rexRunOnLine = new Regex( @"^[^""]*(?:""[^""]*""[^""]*)*""[^""]*$" );

        public CsvReader( string fileName, bool hasHeader ) : this( new FileStream( fileName, FileMode.Open, FileAccess.Read ) )
        {
            __hasHeader = hasHeader;
        }

        public CsvReader( Stream stream )
        {
            __reader = new StreamReader( stream );
        }

        public IEnumerable RowEnumerator
        {
            get {
                if ( null == __reader )
                    throw new ApplicationException( "I can't start reading without CSV input." );

                __rowno = 0;
                string sLine;
                string sNextLine;
                bool headerSkipped = false;

                while ( null != ( sLine = __reader.ReadLine() ) )
                {
                    if (__hasHeader && !headerSkipped)
                    {
                        headerSkipped = true;
                        continue;
                    }
                    
                    while ( rexRunOnLine.IsMatch( sLine ) && null != ( sNextLine = __reader.ReadLine() ) )
                        sLine += "\n" + sNextLine;

                    __rowno++;
                    string[] values = rexCsvSplitter.Split( sLine );

                    for ( int i = 0; i < values.Length; i++ )
                        values[i] = Csv.Unescape( values[i] );

                    yield return values;
                }

                __reader.Close();
            }
        }

        public long RowIndex { get { return __rowno; } }

        public void Dispose()
        {
            if ( null != __reader ) __reader.Dispose();
        }
    }
}