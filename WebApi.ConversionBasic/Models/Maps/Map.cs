using System;
using System.Collections.Generic;

namespace WebApi.ConversionBasic.Models.Maps
{
    public abstract class Map
    {
        /// <summary>
        /// Bắt đầu đọc dữ liệu từ dòng thứ mấy trog file
        /// </summary>
        public int DataFromLine { get; set; }

        /// <summary>
        /// Vị trí dòng kết thúc 1 page
        /// </summary>
        public int BreakPageLine { get; set; }

        /// <summary>
        /// Dấu hiệu nhận biết kết thúc 1 page
        /// </summary>
        public List<Condition> BreakPageCondition { get; set; }

        /// <summary>
        /// Dấu hiệu nhận biết bắt đầu 1 page
        /// </summary>
        public List<Condition> NewPageCondition { get; set; }

        public List<Condition> ValidatePosition { get; set; }

        /// <summary>
        /// CSV file with delimiter and position of Column
        /// </summary>
        public string Delimiter { get; set; }
        public int ColumnNameRow { get; set; }

        public MapHeader MapHeader { get; set; }
        public MapDetail MapDetail { get; set; }
        public MapOverflow MapOverflow { get; set; }

        public MapRemittance MapRemittance { get; set; }
        /// <summary>
        /// Các filed address
        /// </summary>
        public List<string> AddressPreFixs { get; set; }

        public Map()
        {
            this.MapHeader = new MapHeader();
            this.MapDetail = new MapDetail();
            this.AddressPreFixs = new List<string>();
            this.NewPageCondition = new List<Condition>();
            this.BreakPageCondition = new List<Condition>();
            this.ValidatePosition = new List<Condition>();

            this.DataFromLine = 1;
        }

        public virtual void Initialize()
        {
            try
            {
                this.SetMapHeader();
                this.SetMapDetail();
                this.SetMapRemittance();

                if (MapRemittance != null)
                {
                    if (MapRemittance.MapFields.Count == 0)
                        MapRemittance.MapFields = MapDetail.MapFields;
                    if (MapRemittance.DetailCount == 1)
                        MapRemittance.DetailCount = MapDetail.DetailCount;
                    if (MapRemittance.DetailLength == 0)
                        MapRemittance.DetailLength = MapDetail.DetailLength;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Map file is INVALID: " + ex.Message, ex);
            }
        }

        public abstract void SetMapHeader();
        public abstract void SetMapDetail();
        public virtual void SetMapRemittance()
        {

        }
        
    }
}
