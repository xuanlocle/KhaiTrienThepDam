using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienDam
{
    public class CUltilities
    {
        public static void Logger(String lines) 
        {
            //try
            //{
            //    // Write the string to a file.append mode is enabled so that the log
            //    // lines get appended to  test.txt than wiping content and writing the log

            //    //System.IO.StreamWriter file = new System.IO.StreamWriter("C:/logDam/logDam.txt", true);
            //    //file.WriteLine(lines);

            //    //file.Close();
            //}
            //catch(Exception exx)
            //{
            //    //TaskDialog.Show("ERROR LOG", "Không tồn tại thư mục: \"C:\\logDam\"");
            //}

        }
        public const string VersionName = "4.6";
        //public const string BuildDate = "24 Jan, 2019\nFix đai C metro";
        public const string BuildDate = "13 Feb, 2019";

        //NEO THEP DAU DAM
        public const string goi1 = "GỐI 1";
        public const string goi2 = "GỐI 2";
        public const string bCotDau1 = "B_Cột đầu 1";
        public const string bCotDau2 = "B_Cột đầu 2";
        public const string viTriDam = "VI TRI GOI";
        public const string viTriGoi1 = "VI TRI GOI 1";
        public const string viTriGoi2 = "VI TRI GOI 2";

        //THEP CHU
        public const string thepTren1DuongKinh = "D_Thép trên";
        public const string thepTren1SoLuong = "n_Số thép chủ lớp trên";
        public const string thepDuoi1DuongKinh = "D_Thép dưới";
        public const string thepDuoi1SoLuong = "n_Số thép chủ lớp dưới";

        public const string thepTren2Check = "Thép lớp trên 2";
        public const string thepTren2DuongKinh = "D_Thép trên 2";
        public const string thepTren2SoLuong = "n_Số thép chủ lớp trên 2";

        public const string thepDuoi2Check = "Thép lớp dưới 2";
        public const string thepDuoi2DuongKinh = "D_Thép dưới 2";
        public const string thepDuoi2SoLuong = "n_Số thép lớp dưới 2";





        //THEP GIA CUONG
        public const string thepGiaCuongGoi1Check = "Thép gia cường gối 1 lớp 1";
        public const string thepGiaCuongGoi1DuongKinh = "D_Thép gối 1 lớp 1";
        public const string thepGiaCuongGoi1SoLuong = "n_Số thép gia cường gối 1 lớp 1";
        public const string thepGiaCuongGoi1DinhVi = "L1_Định vị thép gối 1 hoàn thiện";

        


        public const string thepGiaCuongGoi1L2Check = "1. Thép gia cường gối đầu 1";
        public const string thepGiaCuongGoi1L2DuongKinh = "D_Thép gối 1";
        public const string thepGiaCuongGoi1L2SoLuong = "n_Số thép gia cường gối 1";

        public const string thepGiaCuongNhipCheck = "Thép gia cường bụng lớp 1";
        public const string thepGiaCuongNhipDuongKinh = "D_Thép gia cường bụng";
        //public const string thepGiaCuongNhipSoLuong = "";

        public const string thepGiaCuongNhipL2Check = "Thép nhịp";
        public const string thepGiaCuongNhipL2DuongKinh = "D_Thép bụng";
        public const string thepGiaCuongNhipL2SoLuong = "n_Số thép nhịp";
        public const string thepGiaCuongNhipDinhVi = "L_Thép nhịp tự do";
        public const string thepGiaCuongNhipLCheck = "Thép nhịp tự do";

        public const string thepGiaCuongGoi2Check = "Thép gia cường gối 2 lớp 1";
        public const string thepGiaCuongGoi2DuongKinh = "D_Thép gối 2 lớp 1";
        public const string thepGiaCuongGoi2SoLuong = "n_Số thép gia cường gối 2 lớp 1";
        public const string thepGiaCuongGoi2DinhVi = "L1_Định vị thép gối 2 hoàn thiện";

        public const string thepGiaCuongGoi2L2Check = "1. Thép gia cường gối đầu 2";
        public const string thepGiaCuongGoi2L2DuongKinh = "D_Thép gối 2";
        public const string thepGiaCuongGoi2L2SoLuong = "n_Số thép gia cường gối 2";

        public const string thepGiaCuongAuTo = "TỰ DO";




        //THEP DAI
        public const string thepDaiCheck = "Thép đai";
        public const string thepDaiGoi1DuongKinh = "D_Thép đai gối 1";
        public const string thepDaiGoi1KhoangRai = "R_Khoảng cách đai gối 1";
        public const string thepDaiNhipDuongKinh = "D_Thép đai nhịp";
        public const string thepDaiNhipKhoangRai = "R_Khoảng cách đai nhịp";
        public const string thepDaiGoi2DuongKinh = "D_Thép đai gối 2";
        public const string thepDaiGoi2KhoangRai = "R_Khoảng cách đai gối 2";



        //THEP DAI GIA CUONG
        public const string thepDaiGiaCuongNhanhTrenCheck = "Thép đai nhánh trên";
        public const string thepDaiGiaCuongDauDam1DuongKinh = "D_Thép đai nhánh trên";
        public const string thepDaiGiaCuongDauDam1KhoangCach = "R_Khoảng cách đai nhánh trên";

        public const string thepDaiGiaCuongNhanhDuoiCheck = "Thép đai nhánh dưới";
        public const string thepDaiGiaCuongDauDam2DuongKinh = "D_Thép đai nhánh dưới";
        public const string thepDaiGiaCuongDauDam2KhoangCach = "R_Khoảng cách đai nhánh dưới";




        //THEP GIA
        public const string thepGiaCheck = "Thép giá";
        public const string thepGiaDuongKinh = "D_Thép giá";

        public const string thepGiaDaiGiaCheck = "Thép đai giá";
        //public const string thepGiaDaiGiaDuongKinh = "D_Thép đai giá ngang";
        public const string thepGiaDaiGiaDuongKinh = "D_Thép đai giá";
        public const string thepGiaDaiGiaDuongKinhGEM = "D_Thép đai giá";
        //public const string thepGiaDaiGiaKhoangCach = "R_Khoảng cách đai giá giữa";
        public const string thepGiaDaiGiaKhoangCach = "R_Khoảng cách đai giá";
        public const string thepGiaDaiGiaKhoangCachGEM = "R_Khoảng cách đai giá";

        public const string thepGiaToiGoi1 = "Thép giá đi tới gối 1";
        public const string thepGiaToiGoi2 = "Thép giá đi tới gối 2";





        //THEP DAI LONG
        public const string thepDaiLongDonCheck = "Thép đai lồng đơn";
        public const string thepDaiLongDonN = "n_Số thép chủ cho đai lồng đơn";
        public const string thepDaiLongKepCheck = "Thép đai kép";
        public const string thepDaiLongKepBen1 = "Thép đai kép 1";
        public const string thepDaiLongKepBen2 = "Thép đai kép 2";
        public const string thepDaiLongKepN1 = "n_Số thép chủ định vị đai lồng kép";
        public const string thepDaiLongKepN2 = "n_Số thép chủ cho đai lồng kép";

        //THEP VAI BO
        public const string thepVaiBoDau1Check = "Thép vai bò đầu 1";
        public const string thepVaiBo1B = "b_Dầm giao đầu 1";
        public const string thepVaiBo1H = "h_Dầm giao đầu 1";
        //public const string thepVaiBo1Check = "";
        public const string thepVaiBo1DaiCheck = "Thép đai tăng cường đầu 1";
        public const string thepVaiBo1DuongKinhDaiTru = "D_Thép đai trừ đầu 1";
        public const string thepVaiBo1KhoangCachDaiTru = "R_Khoảng rải đai trừ đầu 1";

        public const string thepVaiBoDau2Check = "Thép vai bò đầu 2";
        public const string thepVaiBo2B = "b_Dầm giao đầu 2";
        public const string thepVaiBo2H = "h_Dầm giao đầu 2";
        //public const string thepVaiBo2Check = "";
        public const string thepVaiBo2DaiCheck = "Thép đai tăng cường đầu 2";
        public const string thepVaiBo2DuongKinhDaiTru = "D_Thép đai trừ đầu 2";
        public const string thepVaiBo2KhoangCachDaiTru = "R_Khoảng đai trừ đầu 2";



        //THEP DAI C
        public const string thepDaiCDungDonCheck = "Thép đai C giữa";
        //public const string thepDaiCDungGiua = "Thép đai C giữa";
        //public const string thepDaiCDungKepCheck = "Thép đai đứng C thứ 1";
        public const string thepDaiCDungKepCheck = "Thép đai đứng C";
        public const string thepDaiCDungKepBen1 = "Thép đai C1";
        public const string thepDaiCDungKepBen2 = "Thép đai C2";
        //public const string thepDaiCDungKepN = "n_Số thép chủ định vị đai C thứ 1";
        public const string thepDaiCDungKepN = "n_Số thép chủ định vị đai C";


        //metro
        public const string thepDaiCDungKep2Check = "Thép đai đứng C thứ 2";
        public const string thepDaiCDungKep2Ben1 = "Thép đai C1a";
        public const string thepDaiCDungKep2Ben2 = "Thép đai C2a";
        public const string thepDaiCDungKep2N = "n_Số thép chủ định vị đai C thứ 2";


        public const string thepDaiCNgangTrenCheck = "Thép đai nhánh trên";
        public const string thepDaiCNgangTrenD = "D_Thép đai nhánh trên";
        public const string thepDaiCNgangTrenR = "R_Khoảng cách đai nhánh trên";
        public const string thepDaiCNgangDuoiCheck = "Thép đai nhánh dưới";
        public const string thepDaiCNgangDuoiD = "D_Thép đai nhánh dưới";
        public const string thepDaiCNgangDuoiR = "R_Khoảng cách đai nhánh dưới";


        //COT

        //W1
        public const string thepChu1Cham1DThepChu = "D_Thep chu";
        public const string thepChu1Cham1LThepChuBungDauTien = "L_dinh vi thep chu bung";
        public const string thepChu1Cham1NTongThepChu = "n_Tong so luong thep chu";
        public const string thepChu1Cham1NHangThepChuCanhNgan = "n_So hang thep chu canh ngan";

        //W2
        public const string thepChu1Cham2DThepChuCanh = "D thep chu canh";
        public const string thepChu1Cham2NTongSoLuongThepCanh = "n_Tong so luong thep canh";
        public const string thepChu1Cham2NSoHangThepChuCanhNgan = "n_So hang thep chu canh ngan";
        public const string thepChu1Cham2CheckThepGiaCuongCanh = "Thép gia cường cánh";
        //public const string thepChu1Cham2NThepGiaCuongCanh = 

        public const string thepChu1Cham2DThepChuBung = "D_Thep chu bung";
        public const string thepChu1Cham2NThepChuBung = "n_So luong thep chu bung";

        //W3
        public const string thepChuW3D1Tren = "D_Thep tren";
        public const string thepChuW3D1Duoi = "D_Thep duoi";
        public const string thepChuW3NB = "n_So hang thep chu canh ngan";
        public const string thepChuW3NTong = "n_Tong so luong thep chu";
        public const string thepChuW3G = "g_dinh vi thep chu bung";


        //W4
        public const string thepChuW4D1Tren = "D_Thep chu bung tren";
        public const string thepChuW4D1Duoi = "D_Thep chu bung duoi";
        public const string thepChuW4D2Tren = "D_Thep chu canh tren";
        public const string thepChuW4D2Duoi = "D_Thep chu canh duoi";
        public const string thepChuW4NB = "n_So hang thep chu canh ngan";
        public const string thepChuW4N1 = "n_Thep chu bung";
        public const string thepChuW4N2 = "n_Tong so luong thep canh";
        public const string thepChuW4ThepGiaCuongCanhCheck = "Thép gia cường cánh";


        //W7
        public const string thepChu1Cham7DThepChuTren = "D_Thep tren";
        public const string thepChu1Cham7DThepChuDuoi = "D_Thep duoi";
        public const string thepChu1Cham7G = "g_dinh vi thep chu bung";
        public const string thepChu1Cham7NB = "n_So hang thep chu canh ngan";
        public const string thepChu1Cham7NTong = "n_Tong so luong thep chu";
        public const string thepChu1Cham7HDoanNeoThep = "h_Neo";
        public const string thepChu1Cham7LDinhViDoanThepNeo = "L_Be ngang";
        public const string thepChu1Cham7NeoTuDoCheck = "Neo tự do";


        //W8
        public const string thepChu1Cham8DThepChuCanhTren = "D_Thep chu canh tren";
        public const string thepChu1Cham8DThepChuCanhDuoi = "D_Thep chu canh duoi";
        public const string thepChu1Cham8NTongCanh = "n_Tong so luong thep canh";
        public const string thepChu1Cham8NB = "n_So hang thep chu canh ngan";
        public const string thepChu1Cham8CheckThepGiaCuongCanh = "Thép gia cường cánh";
        public const string thepChu1Cham8DThepChuBungTren = "D_Thep chu bung tren";
        public const string thepChu1Cham8DThepChuBungDuoi = "D_Thep chu bung duoi";
        public const string thepChu1Cham8NTongBung = "n_Thep chu bung";
        public const string thepChu1Cham8HDoanNeoThep = "h_Neo";
        public const string thepChu1Cham8LDinhViDoanThepNeo = "L_Be ngang";
        public const string thepChu1Cham8NeoTuDoCheck = "Neo tự do";

        //public const string thepDai2Cham1DThepDaiNgang = "D_Thep ngang";
        //public const string thepDai2Cham1DThepDaiC = "D_Thep dai C";
        //public const string thepDai2Cham1NThepDaiC = "n_Số thép đai C";
        //public const string thepDai2Cham1RThepDaiChanVaDinhCot = "a_Khoảng cách đai đỉnh và chân cột";
        //public const string thepDai2Cham1RThepDaiDoanNoiChong = "a_Khoảng cách đai đoạn nối chồng";
        //public const string thepDai2Cham1RThepDaiGiuaCot = "a_Khoảng cách đai giữa cột";
        //public const string thepDai2Cham1RThepDaiTrongDamSan = "a_Khoảng rãi đai trong dầm sàn";
        //public const string thepDai2Cham1RThepDaiCBung = "a_Khoảng rãi đai C bụng";
        //L1
        public const string thepChuL1DThepChu = "D thep chu";
        public const string thepChuL1NTong = "n_Tong so luong thep chu";
        public const string thepChuL1NCanhL1 = "n_Số hàng thép cạnh L1";
        public const string thepChuL1NB1 = "n_So hang thep chu canh ngan 1";
        public const string thepChuL1NB2 = "n_So hang thep chu canh ngan 2";
        public const string thepChuL1GiaCuongCanh1Check = "Thép gia cường cánh 1";
        public const string thepChuL1GiaCuongCanh2Check = "Thép gia cường cánh 2";

        //L2
        public const string thepChuL2DThepChu = "D thep chu";
        public const string thepChuL2DThepChuBung = "D_Thep chu bung";
        public const string thepChuL2NCanhL1 = "n_Số hàng thép cánh L1";
        public const string thepChuL2NBungL1 = "n_Số hàng thép bụng L1";
        public const string thepChuL2NCanhL2 = "n_Số hàng thép cánh L2";
        public const string thepChuL2NBungL2 = "n2_Số hàng thép bụng L2";
        public const string thepChuL2NB1 = "n_So hang thep chu canh ngan 1";
        public const string thepChuL2NB2 = "n_So hang thep chu canh ngan 2";
        public const string thepChuL2GiaCuongCanh1Check = "Thép gia cường cánh 1";
        public const string thepChuL2GiaCuongCanh2Check = "Thép gia cường cánh 2";

        //L3
        public const string thepChuL3DThepTren = "D_Thep tren";
        public const string thepChuL3DThepDuoi = "D_Thep duoi";
        public const string thepChuL3NTong = "n_Tong so luong thep chu";
        public const string thepChuL3NCanhL1 = "n_Số hàng thép cạnh L1";
        public const string thepChuL3NB1 = "n_So hang thep chu canh ngan 1";
        public const string thepChuL3NB2 = "n_So hang thep chu canh ngan 2";
        public const string thepChuL3GiaCuongCanh1Check = "Thép gia cường cánh 1";
        public const string thepChuL3GiaCuongCanh2Check = "Thép gia cường cánh 2";


        //C1
        public const string thepChuC1DThepChu = "D_Thep chu";
        public const string thepChuC1NTong = "n_Tong so luong thep chu";
        public const string thepChuC1NCanhA = "n_So hang thep chu canh A";

        //DAI
        //W1,3,5,7
        public const string thepDai2Cham1DThepDaiNgang = "D_Thep ngang";
        public const string thepDai2Cham1DThepDaiC = "D_Thep dai C";
        public const string thepDai2Cham1NThepDaiC = "n_Số thép đai C";
        public const string thepDai2Cham1RThepDaiChanVaDinhCot = "a_Khoảng cách đai đỉnh và chân cột";
        public const string thepDai2Cham1RThepDaiDoanNoiChong = "a_Khoảng cách đai đoạn nối chồng";
        public const string thepDai2Cham1RThepDaiGiuaCot = "a_Khoảng cách đai giữa cột";
        public const string thepDai2Cham1RThepDaiTrongDamSan = "a_Khoảng rãi đai trong dầm sàn";
        public const string thepDai2Cham1RThepDaiCBung = "a_Khoảng rãi đai C bụng";

        //W2,4,6,8
        public const string thepDai2Cham2DThepDaiNgang = "D_Thep ngang";
        public const string thepDai2Cham2DThepDai = "D thep dai";
        public const string thepDai2Cham2DThepDaiC = "D_Thep dai C";
        public const string thepDai2Cham2NThepDaiCCanh = "n_Thép đai C cánh";
        public const string thepDai2Cham2NThepDaiCBung = "n_Số thép đai C bụng";
        //public const string thepDai2Cham2NThepDaiCTangCuong = "";
        public const string thepDai2Cham2RThepDaiCBung = "a_Khoảng rãi đai C bụng";
        public const string thepDai2Cham2RThepDaiChanVaDinhCot = "a_Khoảng cách đai đỉnh và chân cột";
        public const string thepDai2Cham2RThepDaiCDoanNoiChong = "a_Khoảng cách đai đoạn nối chồng";
        public const string thepDai2Cham2RThepDaiGiuaCot = "a_Khoảng cách đai giữa cột";
        public const string thepDai2Cham2RThepDaiTrongDamSan = "a_Khoảng rãi đai trong dầm sàn";
        public const string thepDai2Cham2NoiThepDaiCheck = "Nối thép đai";

        //L1,2,3
        public const string thepDaiL1DThepDai = "D thep dai";
        public const string thepDaiL1DThepDaiC = "D_Thep dai C";
        public const string thepDaiL1RNoiChong = "a_Khoảng cách đai đoạn nối chồng";
        public const string thepDaiL1RGiuaCot = "a_Khoảng cách đai giữa cột";
        public const string thepDaiL1RDinhVaChanCot = "a_Khoảng cách đai đỉnh và chân cột";
        public const string thepDaiL1RTrongDamSan = "a_Khoảng rãi đai trong dầm sàn";
        public const string thepDaiL1NDaiCCanh1 = "n_Thép đai C canh 1";
        public const string thepDaiL1NDaiCCanh2 = "n_Số thép đai C canh 2";


        //C1
        public const string thepDaiC1ThepNgang = "D_ThepNgang";
        public const string thepDaiC1RDoanNoiChong = "a_Khoảng rãi đai đoạn nối chồng";
        public const string thepDaiC1RGiuaCot = "a_Khoảng rãi đai giữa cột";
        public const string thepDaiC1RDinhVaChanCot = "a_Khoảng rãi đai đỉnh và chân cột";
        public const string thepDaiC1RTrongDamSan = "a_Khoảng rãi đai trong dầm sàn";
        public const string thepDaiC1RaiKhacThepDaiChinhCheck = "Thép đai phụ rãi khác thép đai chính";
        public const string thepDaiC1RDaiPhu = "Khoảng rãi thép đai phụ";
        public const string thepDaiC1DDaiLong = "D_Dai long";
        public const string thepDaiC1NDaiLongCanhA = "n_Số đai lồng cạnh A";
        public const string thepDaiC1NDaiLongCanhB = "n_Số đai lồng cạnh B";
        public const string thepDaiC1DThepDaiC = "D_Thep dai C";
        public const string thepDaiC1NDaiCCanhA = "n_Số thép đai C cạnh A";
        public const string thepDaiC1NDaiCCanhB = "n_Số thép đai C cạnh B";


        //Addin Dam 2 
        public const string thepGiaCuongGoi1DinhViD2 = "L_Định vị thép gối 1";
        public const string thepGiaCuongGoi2DinhViD2 = "L_Định vị thép gối 2";
        public const string thepGiaCuongNhipDinhViD2 = "L_Thép nhịp tự do";

        public const string thepDaiGoi1DinhViD2 = "a_Khoảng rải đai gối 1";

        public const string thepGiaCuongNhipCheckD2 = "Thép gia cường nhịp lớp 1";
        public const string thepGiaCuongNhipL2CheckD2 = "Thép gia cường nhịp lớp 2";
        //public const string thepDaiGoi2DinhViD2 = "L1_Định vị thép gối 2";

        public const string thepGiaCuongNhipDuongKinhD2 = "D_Thép gia cường nhịp lớp 1";
        public const string thepGiaCuongNhipL2DuongKinhD2 = "D_Thép gia cường nhịp lớp 2";
        public const string thepGiaCuongNhipL2SoLuongD2 = "n_Số thép nhịp lớp 2";

        public static double? GetValueFromString(string text)
        {
            string str;
            if (text.Contains(" mm"))
            {
                str = text.Replace(" mm", "");
            }
            else if (text.Contains("mm"))
            {
                str = text.Replace("mm", "");
            }
            else
            {
                str = text;
            }
            if (double.TryParse(str, out double num))
            {
                return new double?(num);
            }
            return null;
        }

        public static double? GetValueFromString(string text, Units units)
        {
            //Check the string value
            string heightValueString = text;
            double lenght;

            if (Autodesk.Revit.DB.UnitFormatUtils.TryParse(units, UnitType.UT_Length, heightValueString, out lenght))
            {
                return lenght;
            }
            else
            {
                return null;
            }
        }
    }
}
