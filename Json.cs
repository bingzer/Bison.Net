using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bingzer.Bison.Commons;

namespace Bingzer.Bison
{
    /// <summary>
    /// Json
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name="Json", Browsable=false)]
    public abstract class Json
    {
        #region Private Data Members
        private Json parent = null;
        private string name = null;
        private bool browsable = true;
        private object value = null;
        #endregion
        #region Public Properties
        /// <summary>
        /// The parent
        /// </summary>
        public Json Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        /// <summary>
        /// The name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Is browsable
        /// </summary>
        public bool IsBrowsable
        {
            get
            {
                return browsable;
            }
            set
            {
                browsable = value;
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion

        /// <summary>
        /// New instance of Json
        /// </summary>
        /// <param name="parent"></param>
        internal Json(Json parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Find the JsonObject root
        /// </summary>
        /// <returns></returns>
        public Json Root()
        {
            Json root = this;
            while (root.parent != null)
                root = root.parent;
            // return
            return root;
        }

        /// <summary>
        /// Returns any parent that has the specific name.
        /// If no such parent found, it should return the immediate parent.
        /// <code>name</code> is always case-sensitive
        /// </summary>
        /// <param name="name">The name of the parent</param>
        /// <returns></returns>
        public Json ParentWith(string name)
        {
            Json p = parent;
            while (p != null && p.name != null)
            {
                if (p.name == name) return p;
                p = parent.parent;
            }
            return p;
        }

        /// <summary>
        /// Returns the number count until,
        /// this object reaches the root object
        /// </summary>
        /// <returns></returns>
        internal int RootCount()
        {
            int rootCount = 0;
            Json json = this;
            while (json.parent != null)
            {
                rootCount++;
                json = json.parent;
            }

            return rootCount;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <param name="tabChar"></param>
        /// <returns></returns>
        public string ToString(string tabChar)
        {
            return ToString(tabChar, Constants.NEW_LINE);
        }

        #region Abstract Methods
        public abstract Json Pair(string name);
        public abstract Json Pair(string name, object value);
        public abstract string ToString(string tabChar, string lineChar);
        internal abstract Json Parse(string jsonText);
        #endregion
        #region Static Methods/Properties

        /// <summary>
        /// Creates JsonObject
        /// </summary>
        public const string OBJECT = Constants.OBJECT;

        /// <summary>
        /// Creates JsonArray
        /// </summary>
        public const string ARRAY = Constants.ARRAY;

        /// <summary>
        /// Creates JsonPair
        /// </summary>
        public const string PAIR = Constants.PAIR;

        /// <summary>
        /// Should be called when thrown error
        /// </summary>
        /// <param name="json"></param>
        /// <param name="jsonText"></param>
        internal static void SyntaxError(Json json, string jsonText)
        {
            string text = "<unknown>";
            if (json != null)
            {
                if (json.parent != null)
                    text = json.parent.ToString();
                else text = json.ToString();
            }
            String message = "Syntax Error\n\t" + text + " at " + jsonText;
            throw new JsonException(message);
        }

        /// <summary>
        /// Jsonify
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static Json Jsonify(string jsonText)
        {
            return Jsonify((object)jsonText);
        }

        /// <summary>
        /// Jsonify
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static Json Jsonify(object any)
        {
            return Jsonify(null, any);
        }

        /// <summary>
        /// Jsonify
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static Json Jsonify(Json parent, string jsonText)
        {
            return Jsonify(null, (object)jsonText);
        }

        /// <summary>
        /// Jsonify
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="any"></param>
        /// <returns></returns>
        public static Json Jsonify(Json parent, object any)
        {
            Json json = null;
            string jsonText = null;
        
            if(ObjectUtil.IsNull(any))
                json = new JsonNull(parent); // no need to parse
            else if(ObjectUtil.IsCharacter(any)){
                // build character
                jsonText = new StringBuilder().Append(Constants.DOUBLE_QUOTE)
                    .Append(any).Append(Constants.DOUBLE_QUOTE).ToString();
                json = new JsonString(parent).Parse(jsonText); 
            }
            // ---- boolean
            else if(ObjectUtil.IsBoolean(any))
                json = new JsonBoolean(parent).Parse(any.ToString());
            // ---- Number
            else if(ObjectUtil.IsNumber(any))
                json = new JsonNumber(parent).Parse(any.ToString());
            // ---- Date
            else if(ObjectUtil.IsDateTime(any)){
                // build json date
                // ex: new Date(value) where value is 'long' value
                jsonText = new StringBuilder(Constants.NEW).Append(Constants.SPACE).Append(Constants.DATE)
                            .Append(Constants.PARENTHESE_START)
                            .Append(((long)((DateTime)any).Ticks).ToString())
                            .Append(Constants.PARENTHESE_END).ToString();
                json = new JsonDate(parent).Parse(jsonText);
            }
            // ---- Other json object
            else if(any is Json){
                json = (Json)any;
            }
            // ---- Array
            else if(ObjectUtil.IsObjectArray(any)){
                object[] array = Global.ToObjectArray(any);
            
                json = new JsonArray(parent);
                // build the json array
                StringBuilder builder = new StringBuilder().Append(Constants.ARRAY_START);
                for(int i = 0; i < array.Length; i++){
                    Json child = Jsonify(json, array[i]);
                    builder.Append(child);
                    // append comma
                    if(i != array.Length - 1)
                        builder.Append(Constants.COMMA);
                }
                builder.Append(Constants.ARRAY_END);
                jsonText = builder.ToString();
                json.Parse(jsonText);
            }
            // --- Java enum.. 
            else if(any.GetType().IsEnum){
                // conver this to string..
                any = new StringBuilder().Append(Constants.DOUBLE_QUOTE).Append(any).Append(Constants.DOUBLE_QUOTE).ToString();
                json = new JsonString(parent).Parse(any.ToString());
            }
            // --- char sequence
            else if(any is string || any is StringBuilder){
                if(any is StringBuilder)
                    any = any.ToString();

                jsonText = any.ToString().Trim();
                // see if it starts with \" or just '
                if(JsonUtil.IsString(jsonText)) json = new JsonString(parent).Parse(jsonText);            
                else if(jsonText == OBJECT) json = new JsonObject(parent);
                else if(jsonText == ARRAY) json = new JsonArray(parent);
                else if(jsonText == PAIR) json = new JsonPair(parent);
                else {
                    // string..
                    if(jsonText.Length > 0){
                        if(JsonUtil.IsString(jsonText))
                            json = new JsonString(parent);
                        else if(JsonUtil.IsJsonObject(jsonText))
                            json = new JsonObject(parent);
                        else if(JsonUtil.IsJsonArray(jsonText))
                            json = new JsonArray(parent);
                        else if(JsonUtil.IsBoolean(jsonText))
                            json = new JsonBoolean(parent);
                        else if(JsonUtil.IsNull(jsonText))
                            json = new JsonNull(parent);
                        else if(JsonUtil.IsNumber(jsonText))
                            json = new JsonNumber(parent);
                        else if(JsonUtil.IsDate(jsonText))
                            json = new JsonDate(parent);
                        else
                            // -- anything else
                            json = new JsonPair(parent);
                    }
                    // -- totally empty space
                    //else throw new JsonException("Value is empty");
                    // append..
                    json.Parse(jsonText);
                }
        
            }// end if charsequence
            // ---- Other java object.. mm.. what to do..
            else{
                // must distinguish between creating a pair or object..           
                // TODO:
                json = new JsonObject(parent);
            
                
                if(any.GetType().GetCustomAttributes(typeof(Entity), true) != null){
                    json.IsBrowsable = ((Entity)any.GetType().GetCustomAttributes(typeof(Entity), true)[0]).Browsable;
                }
            
                if(json.IsBrowsable){
                    //TODO
                    //json = Options.getDefault().getObjectifier().toJson(any, json);
                }
            }
        
            return json;
        }

        #endregion
    }
}
