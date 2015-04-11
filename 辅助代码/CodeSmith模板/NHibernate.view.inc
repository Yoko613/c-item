<script runat="template">
private Regex cleanRegEx = new Regex(@"\s+|_|-|\.", RegexOptions.Compiled);
        private Regex cleanID = new Regex(@"(_ID|_id|_Id|\.ID|\.id|\.Id|ID|Id)", RegexOptions.Compiled);

        public string CleanName(string name)
        {
            return cleanRegEx.Replace(name, "");
        }

        public string CamelCase(string name)
        {
            string output = CleanName(name);
            return char.ToLower(output[0]) + output.Substring(1);
        }

        public string CamelCaseProperty(string name)
        {
            string output = "";
            string[] outputs = name.Split('_');
            foreach (string s in outputs)
            {
                output += char.ToUpper(s[0]) + s.Substring(1).ToLower() + "_";
            }
            return output.TrimEnd('_');
        }
        
        public string CamelCaseClass(string name)
        {
            string output = "";
            string[] outputs = name.Split('_');
            foreach (string s in outputs)
            {
                output += char.ToUpper(s[0]) + s.Substring(1).ToLower() + "_";
            }
            return output.TrimEnd('_');
        }
        public string PascalCase(string name)
        {
            //string output = CleanName(name);
            //return char.ToUpper(output[0]) + output.Substring(1).ToLower();

            return CamelCaseClass(name);
        }

        public string MakePlural(string name)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(name))
                return plural1.Replace(name, "${keep}ies");
            else if (plural2.IsMatch(name))
                return plural2.Replace(name, "${keep}s");
            else if (plural3.IsMatch(name))
                return plural3.Replace(name, "${keep}es");
            else if (plural4.IsMatch(name))
                return plural4.Replace(name, "${keep}s");

            return name;
        }

        public string MakeSingle(string name)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(name))
                return plural1.Replace(name, "${keep}y");
            else if (plural2.IsMatch(name))
                return plural2.Replace(name, "${keep}");
            else if (plural3.IsMatch(name))
                return plural3.Replace(name, "${keep}");
            else if (plural4.IsMatch(name))
                return plural4.Replace(name, "${keep}");

            return name;
        }

        public string CSharpType(ViewColumnSchema column)
        {
            if (column.Name.EndsWith("TypeCode")) return column.Name;

            switch (column.DataType)
            {
                case DbType.AnsiString: return "string";
                case DbType.AnsiStringFixedLength: return "string";
                case DbType.Binary: return "byte[]";
                case DbType.Boolean: return "bool";
                case DbType.Byte: return "byte";
                case DbType.Currency: return "decimal";
                case DbType.Date: return "DateTime";
                case DbType.DateTime: return "DateTime";
                case DbType.Decimal: return "decimal";
                case DbType.Double: return "double";
                case DbType.Guid: return "Guid";
                case DbType.Int16: return "short";
                case DbType.Int32: return "int";
                case DbType.Int64: return "long";
                case DbType.Object: return "object";
                case DbType.SByte: return "sbyte";
                case DbType.Single: return "float";
                case DbType.String: return "string";
                case DbType.StringFixedLength: return "string";
                case DbType.Time: return "TimeSpan";
                case DbType.UInt16: return "ushort";
                case DbType.UInt32: return "uint";
                case DbType.UInt64: return "ulong";
                case DbType.VarNumeric: return "decimal";
                default:
                    {
                        return "__UNKNOWN__" + column.NativeType;
                    }
            }
        }

        public string NHibernateType(ViewColumnSchema column)
        {
            if (column.Name.EndsWith("TypeCode")) return column.Name;

            switch (column.DataType)
            {
                case DbType.AnsiString: return "String";
                case DbType.AnsiStringFixedLength: return "String";
                case DbType.Binary: return "Byte[]";
                case DbType.Boolean: return "Boolean";
                case DbType.Byte: return "Byte";
                case DbType.Currency: return "Decimal";
                case DbType.Date: return "DateTime";
                case DbType.DateTime: return "DateTime";
                case DbType.Decimal: return "Decimal";
                case DbType.Double: return "Double";
                case DbType.Guid: return "Guid";
                case DbType.Int16: return "Int16";
                case DbType.Int32: return "Int32";
                case DbType.Int64: return "Int64";
                case DbType.Object: return "BinaryBlob";
                case DbType.SByte: return "Byte";
                case DbType.Single: return "Single";
                case DbType.String: return "String";
                case DbType.StringFixedLength: return "String";
                case DbType.Time: return "DateTime";
                case DbType.UInt16: return "Int16";
                case DbType.UInt32: return "Int32";
                case DbType.UInt64: return "Int64";
                case DbType.VarNumeric: return "Decimal";
                default:
                    {
                        return "__UNKNOWN__" + column.NativeType;
                    }
            }
        }

        private Regex sqlCharacters = new Regex(@"[\s|~|-|!|{|%|}|\^|'|&|.|\(|\\|\)|`]", RegexOptions.Compiled);
        private Regex sqlReserved = new Regex(@"^(ADD|EXCEPT|PERCENT|ALL|EXEC|PLAN|ALTER|EXECUTE|PRECISION|AND|EXISTS|PRIMARY|ANY|EXIT|PRINT|AS|FETCH|PROC|ASC|FILE|PROCEDURE|AUTHORIZATION|FILLFACTOR|PUBLIC|BACKUP|FOR|RAISERROR|BEGIN|FOREIGN|READ|BETWEEN|FREETEXT|READTEXT|BREAK|FREETEXTTABLE|RECONFIGURE|BROWSE|FROM|REFERENCES|BULK|FULL|REPLICATION|BY|FUNCTION|RESTORE|CASCADE|GOTO|RESTRICT|CASE|GRANT|RETURN|CHECK|GROUP|REVOKE|CHECKPOINT|HAVING|RIGHT|CLOSE|HOLDLOCK|ROLLBACK|CLUSTERED|IDENTITY|ROWCOUNT|COALESCE|IDENTITY_INSERT|ROWGUIDCOL|COLLATE|IDENTITYCOL|RULE|COLUMN|IF|SAVE|COMMIT|IN|SCHEMA|COMPUTE|INDEX|SELECT|CONSTRAINT|INNER|SESSION_USER|CONTAINS|INSERT|SET|CONTAINSTABLE|INTERSECT|SETUSER|CONTINUE|INTO|SHUTDOWN|CONVERT|IS|SOME|CREATE|JOIN|STATISTICS|CROSS|KEY|SYSTEM_USER|CURRENT|KILL|TABLE|CURRENT_DATE|LEFT|TEXTSIZE|CURRENT_TIME|LIKE|THEN|CURRENT_TIMESTAMP|LINENO|TO|CURRENT_USER|LOAD|TOP|CURSOR|NATIONAL||TRAN|DATABASE|NOCHECK|TRANSACTION|DBCC|NONCLUSTERED|TRIGGER|DEALLOCATE|NOT|TRUNCATE|DECLARE|NULL|TSEQUAL|DEFAULT|NULLIF|UNION|DELETE|OF|UNIQUE|DENY|OFF|UPDATE|DESC|OFFSETS|UPDATETEXT|DISK|ON|USE|DISTINCT|OPEN|USER|DISTRIBUTED|OPENDATASOURCE|VALUES|DOUBLE|OPENQUERY|VARYING|DROP|OPENROWSET|VIEW|DUMMY|OPENXML|WAITFOR|DUMP|OPTION|WHEN|ELSE|OR|WHERE|END|ORDER|WHILE|ERRLVL|OUTER|WITH|ESCAPE|OVER|WRITETEXT)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Regex sqlFuture = new Regex(@"^(ABSOLUTE|FOUND|PRESERVE|ACTION|FREE|PRIOR|ADMIN|GENERAL|PRIVILEGES|AFTER|GET|READS|AGGREGATE|GLOBAL|REAL|ALIAS|GO|RECURSIVE|ALLOCATE|GROUPING|REF|ARE|HOST|REFERENCING|ARRAY|HOUR|RELATIVE|ASSERTION|IGNORE|RESULT|AT|IMMEDIATE|RETURNS|BEFORE|INDICATOR|ROLE|BINARY|INITIALIZE|ROLLUP|BIT|INITIALLY|ROUTINE|BLOB|INOUT|ROW|BOOLEAN|INPUT|ROWS|BOTH|INT|SAVEPOINT|BREADTH|INTEGER|SCROLL|CALL|INTERVAL|SCOPE|CASCADED|ISOLATION|SEARCH|CAST|ITERATE|SECOND|CATALOG|LANGUAGE|SECTION|CHAR|LARGE|SEQUENCE|CHARACTER|LAST|SESSION|CLASS|LATERAL|SETS|CLOB|LEADING|SIZE|COLLATION|LESS|SMALLINT|COMPLETION|LEVEL|SPACE|CONNECT|LIMIT|SPECIFIC|CONNECTION|LOCAL|SPECIFICTYPE|CONSTRAINTS|LOCALTIME|SQL|CONSTRUCTOR|LOCALTIMESTAMP|SQLEXCEPTION|CORRESPONDING|LOCATOR|SQLSTATE|CUBE|MAP|SQLWARNING|CURRENT_PATH|MATCH|START|CURRENT_ROLE|MINUTE|STATE|CYCLE|MODIFIES|STATEMENT|DATA|MODIFY|STATIC|DATE|MODULE|STRUCTURE|DAY|MONTH|TEMPORARY|DEC|NAMES|TERMINATE|DECIMAL|NATURAL|THAN|DEFERRABLE|NCHAR|TIME|DEFERRED|NCLOB|TIMESTAMP|DEPTH|NEW|TIMEZONE_HOUR|DEREF|NEXT|TIMEZONE_MINUTE|DESCRIBE|NO|TRAILING|DESCRIPTOR|NONE|TRANSLATION|DESTROY|NUMERIC|TREAT|DESTRUCTOR|OBJECT|TRUE|DETERMINISTIC|OLD|UNDER|DICTIONARY|ONLY|UNKNOWN|DIAGNOSTICS|OPERATION|UNNEST|DISCONNECT|ORDINALITY|USAGE|DOMAIN|OUT|USING|DYNAMIC|OUTPUT|VALUE|EACH|PAD|VARCHAR|END-EXEC|PARAMETER|VARIABLE|EQUALS|PARAMETERS|WHENEVER|EVERY|PARTIAL|WITHOUT|EXCEPTION|PATH|WORK|EXTERNAL|POSTFIX|WRITE|FALSE|PREFIX|YEAR|FIRST|PREORDER|ZONE|FLOAT|PREPARE)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string SqlIdentifier(string sqlIdentifier)
        {
            if (sqlCharacters.IsMatch(sqlIdentifier) || sqlReserved.IsMatch(sqlIdentifier) || sqlFuture.IsMatch(sqlIdentifier))
                return String.Format("`{0}`", sqlIdentifier);
            else
                return sqlIdentifier;
        }

        public string ViewClass(ViewSchema view)
        {
            string className = view.Name;
            if (className.StartsWith(RemoveViewPrefix))
                className = className.Substring(RemoveViewPrefix.Length);
            return String.Format("{0}", MakeSingle(PascalCase(className)));
        }

        public string ViewClassFull(ViewSchema view)
        {
            return String.Format("{0}.{1}, {2}", Namespace, ViewClass(view), Assembly);
        }

        public string ViewCollection(ViewSchema view)
        {
            string className = view.Name;
            if (className.StartsWith(RemoveViewPrefix))
                className = className.Substring(RemoveViewPrefix.Length);
            return String.Format("{0}", MakePlural(CamelCaseProperty(className)));
        }
        public string ClassName(ViewSchema view)
        {
            return ViewClass(view);
        }
        public string ClassNameAtt(ViewSchema view)
        {
            return String.Format(" name=\"{0}\"", ViewClassFull(view));
        }
        public string ClassView(ViewSchema view)
        {
            return view.Name;
        }
        public string ClassViewAtt(ViewSchema view)
        {
            return String.Format(" table=\"{0}\"", SqlIdentifier(view.Name));
        }

        public string PropertyName(ViewColumnSchema column)
        {
            return CamelCaseProperty(column.Name);
        }
        public string MemberName(ViewColumnSchema column)
        {
            return "_" + CamelCaseProperty(column.Name);
        }
        public string ParameterName(ViewColumnSchema column)
        {
            return CamelCaseProperty(column.Name);
        }
        public string PropertyNameAtt(ViewColumnSchema column)
        {
            return String.Format(" name=\"{0}\"", PropertyName(column));
        }
        public string PropertyType(ViewColumnSchema column)
        {
            return NHibernateType(column);
        }
        public string MemberType(ViewColumnSchema column)
        {
            return CSharpType(column);
        }
        public string PropertyTypeAtt(ViewColumnSchema column)
        {
            return String.Format(" type=\"{0}\"", PropertyType(column));
        }

        public string ColumnName(ViewColumnSchema column)
        {
            return column.Name;
        }
        public string ColumnNameAtt(ViewColumnSchema column)
        {
            return String.Format(" name=\"{0}\"", SqlIdentifier(ColumnName(column)));
        }
        public string ColumnLength(ViewColumnSchema column)
        {
            if (column.Size > 0)
                return column.Size.ToString();
            else
                return String.Empty;
        }
        public string ColumnLengthAtt(ViewColumnSchema column)
        {
            if (column.Size > 0)
                return String.Format(" length=\"{0}\"", column.Size);
            else
                return String.Empty;
        }
        public string ColumnSqlTypeAtt(ViewColumnSchema column)
        {
            return String.Format(" sql-type=\"{0}\"", column.NativeType);
        }
        public string ColumnNotNullAtt(ViewColumnSchema column)
        {
            return String.Format(" not-null=\"{0}\"", (!column.AllowDBNull).ToString().ToLower());
        }

</script>        