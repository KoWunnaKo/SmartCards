using Kadastr.CoreLib.Attributes;
using System;
using System.Xml.Serialization;

namespace Kadastr.CoreLib
{
    [Serializable]
    [XmlRoot("ResponseFromKadastr")]
    public class OutputKadastrInfo
    {
        [XmlElement("ID")]
        [ApiInfo("Уникальный номер запроса", typeof(long), 12, "ID", RequiredFlags.Mondatory)]
        public long id { get; set; }

        [XmlElement("ID_REQ")]
        [ApiInfo("Уникальный номер ответа запроса", typeof(string), 36, "ID_REQ", RequiredFlags.Mondatory)]
        public string id_req { get; set; }

        [XmlElement("KADASTR_NUMBER")]
        [ApiInfo("Кадастровый номер объекта", typeof(string), 30, "KADASTR_NUMBER", RequiredFlags.Mondatory)]
        public string kadastr_number { get; set; }

        [XmlElement("REGION_NAME")]
        [ApiInfo("Регион", typeof(string), 20, "REGION_NAME", RequiredFlags.Mondatory)]
        public string region_name { get; set; }

        [XmlElement("REGION_ID")]
        [ApiInfo("Код региона", typeof(string), 4, "REGION_ID", RequiredFlags.Mondatory)]
        public string region_id { get; set; }

        [XmlElement("AREA_ID")]
        [ApiInfo("Код района", typeof(string), 4, "AREA_ID", RequiredFlags.Mondatory)]
        public string area_id { get; set; }

        [XmlElement("NAME_FIO")]
        [ApiInfo("Наименование юридического лица", typeof(string), 100, "NAME_FIO", RequiredFlags.Mondatory, "A8")]
        public string name_yur_liso { get; set; }

        [XmlElement("NAME_FIO_2")]
        [ApiInfo("ИНН юридического лица", typeof(string), 100, "NAME_FIO", RequiredFlags.Mondatory, "B1")]
        public string inn_yur_liso { get; set; }

        [XmlElement("NAME_FIO_3")]
        [ApiInfo("Фамилия, имя и отчество граждан", typeof(string), 100, "NAME_FIO", RequiredFlags.Mondatory, "A8")]
        public string fio_grajdan { get; set; }

        [XmlElement("NAME_FIO_4")]
        [ApiInfo("Паспорт серия и номер граждан", typeof(string), 100, "NAME_FIO", RequiredFlags.Mondatory, "A9")]
        public string passport_seria_nomer { get; set; }

        [XmlElement("NAME_FIO_5")]
        [ApiInfo("ПИНФЛ граждан", typeof(string), 100, "NAME_FIO", RequiredFlags.Mondatory)]
        public string pnfl_fizliso { get; set; }

        [XmlElement("KAD_NO")]
        [ApiInfo("Кадастровый номер земельного участка", typeof(string), 22, "KAD_NO", RequiredFlags.Mondatory, "A2")]
        public string kad_no { get; set; }

        [XmlElement("AREA")]
        [ApiInfo("Площадь, га", typeof(float), 11, "AREA", RequiredFlags.Mondatory, "A5")]
        public float area { get; set; }

        [XmlElement("LOCATION")]
        [ApiInfo("Местоположение", typeof(string), 100, "LOCATION", RequiredFlags.Mondatory, "A4")]
        public string location { get; set; }

        [XmlElement("OWN_TYPE")]
        [ApiInfo("Вид права", typeof(string), 100, "OWN_TYPE", RequiredFlags.Mondatory, "A1")]
        public string own_type { get; set; }

        [XmlElement("LAND_DOC")]
        [ApiInfo("Документы, подтверждающие права на земельный участок ", typeof(long), 12, "LAND_DOC", RequiredFlags.Mondatory)]
        public long land_doc { get; set; }

        [XmlElement("LAND_PURP")]
        [ApiInfo("Целевое назначение земельного участка", typeof(string), 100, "LAND_PURP", RequiredFlags.Mondatory)]
        public string land_purp { get; set; }

        [XmlElement("LAND_RESTR")]
        [ApiInfo("Ограничения прав на земельный участок", typeof(bool), 1, "LAND_RESTR", RequiredFlags.Mondatory, "B5")]
        public bool land_restr { get; set; }

        [XmlElement("EST_PRICE")]
        [ApiInfo("Кадастровая оценка, тыс. сум.", typeof(decimal), 15, "EST_PRICE", RequiredFlags.Mondatory, "A6")]
        public decimal est_price { get; set; }

        [XmlElement("REG_D_N")]
        [ApiInfo("Дата и номер регистрации", typeof(string), 50, "REG_D_N", RequiredFlags.Mondatory, "A7")]
        public string reg_d_n { get; set; }

        [XmlElement("CERT_S_N")]
        [ApiInfo("Серия и номер свидетельства", typeof(string), 17, "CERT_S_N", RequiredFlags.Mondatory)]
        public string cert_s_n { get; set; }

        [XmlElement("SERVITUDES")]
        [ApiInfo("Сервитуты (постоянное использование, аренда и т.д.)", typeof(string), 100, "SERVITUDES", RequiredFlags.Mondatory)]
        public string servitudes { get; set; }

        [XmlElement("B_KAD_NO")]
        [ApiInfo("Кадастровый номер здания и сооружения", typeof(string), 22, "B_KAD_NO", RequiredFlags.Mondatory, "A3")]
        public string b_kad_no { get; set; }

        [XmlElement("B_AREA_TOTAL")]
        [ApiInfo("Площадь, м2", typeof(double), 12, "B_AREA_TOTAL", RequiredFlags.Mondatory)]
        public double b_area_total { get; set; }

        [XmlElement("B_AREA_OBSH")]
        [ApiInfo("Площадь, обитая", typeof(double), 12, "B_AREA_OBSH", RequiredFlags.Mondatory)]
        public double b_area_obsh { get; set; }

        [XmlElement("B_AREA_PROIZV")]
        [ApiInfo("Площадь, производственная", typeof(double), 12, "B_AREA_PROIZV", RequiredFlags.Mondatory)]
        public double b_area_proizv { get; set; }

        [XmlElement("B_AREA_JILAYA")]
        [ApiInfo("Площадь, жилая", typeof(double), 12, "B_AREA_JILAYA", RequiredFlags.Mondatory)]
        public double b_area_jilaya { get; set; }

        [XmlElement("B_OWN_TYPE")]
        [ApiInfo("Вид права", typeof(string), 100, "B_OWN_TYPE", RequiredFlags.Mondatory, "B3")]
        public string b_own_type { get; set; }

        [XmlElement("B_LAND_DOC")]
        [ApiInfo("Документы, подтверждающие права на земельный участок", typeof(long), 12, "B_LAND_DOC", RequiredFlags.Mondatory, "B2")]
        public long b_land_doc { get; set; }

        [XmlElement("B_DOLYA")]
        [ApiInfo("Доля собственности в здании и сооружении, часть", typeof(string), 20, "B_DOLYA", RequiredFlags.Mondatory, "B4")]
        public string b_dolya { get; set; }

        [XmlElement("B_RESTRIC")]
        [ApiInfo("Ограничения прав на здания и сооружения", typeof(bool), 1, "B_RESTRIC", RequiredFlags.Mondatory)]
        public bool b_restric { get; set; }

        [XmlElement("B_INV_STOIM")]
        [ApiInfo("Инвентаризационная стоимость, тыс.", typeof(decimal), 15 , "B_INV_STOIM", RequiredFlags.Mondatory)]
        public decimal b_inv_stoim { get; set; }

        [XmlElement("B_REG_D_N")]
        [ApiInfo("Дата и номер регистрации", typeof(string), 50, "B_REG_D_N", RequiredFlags.Mondatory)]
        public string b_reg_d_n { get; set; }

        [XmlElement("B_CERT_S_N")]
        [ApiInfo("Серия и номер свидетельства", typeof(string), 17, "B_CERT_S_N", RequiredFlags.Mondatory)]
        public string b_cert_s_n { get; set; }

        [XmlElement("REGISTR_SUBJ")]
        [ApiInfo("Предмет регистрации", typeof(string), 100, "REGISTR_SUBJ", RequiredFlags.Mondatory)]
        public string registr_subj { get; set; }

        [XmlElement("TREE_QUANT")]
        [ApiInfo("Количество деревьев", typeof(string), 15, "TREE_QUANT", RequiredFlags.Mondatory)]
        public long tree_quant { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}-{12}-{13}-{14}-{15}-{16}-{17}-{18}-{19}-{20}-{21}-{22}-{23}-{24}-{25}-{26}-{27}-{28}-{29}-{30}-{31}-{32}-{33}-{34}-{35}",
                id, id_req, kadastr_number, region_name, region_id, area_id, name_yur_liso, inn_yur_liso,
                fio_grajdan, passport_seria_nomer, pnfl_fizliso, kad_no, area, location, own_type, land_doc,
                land_purp, land_restr, est_price, reg_d_n, cert_s_n, servitudes , b_kad_no, b_area_total,
                b_area_obsh, b_area_proizv, b_area_jilaya, b_own_type, b_land_doc, b_dolya, b_restric,
                b_inv_stoim, b_reg_d_n, b_cert_s_n, registr_subj, tree_quant);
        }
    }
}