using System;
using System.Collections.Generic;

class Rekening
{
    public string Nomor { get; set; }
    public string Nama { get; set; }
    public decimal Saldo { get; set; }

    public Rekening(string Nomor, string Nama, decimal Saldo)
    {
        this.Nomor = Nomor;
        this.Nama = Nama;
        this.Saldo = Saldo;
    }

    public void Data_Nasabah()
    {
        Console.WriteLine($"Nomor Rekening : {this.Nomor}");
        Console.WriteLine($"Nama Pemilik   : {this.Nama}");
        Console.WriteLine($"Saldo Rekening : Rp{this.Saldo:N2}");
        Console.WriteLine(new string('-', 30));
    }

    public void Setor_Dana(decimal Jumlah)
    {
        this.Saldo += Jumlah;
        Console.WriteLine($"Setoran berhasil. Saldo sekarang: Rp{this.Saldo:N2}");
    }

    public void Tarik_Dana(decimal Jumlah)
    {
        if (Jumlah > this.Saldo)
        {
            Console.WriteLine("Saldo tidak mencukupi.");
        }
        else
        {
            this.Saldo -= Jumlah;
            Console.WriteLine($"Penarikan berhasil. Sisa saldo: Rp{this.Saldo:N2}");
        }
    }

    public void Transfer_Dana(Rekening Tujuan, decimal Jumlah)
    {
        if (Jumlah > this.Saldo)
        {
            Console.WriteLine("Saldo tidak mencukupi untuk transfer.");
        }
        else
        {
            this.Saldo -= Jumlah;
            Tujuan.Saldo += Jumlah;
            Console.WriteLine($"Transfer Rp{Jumlah:N2} ke {Tujuan.Nama} berhasil.");
        }
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, Rekening> Data_Rekening = new Dictionary<string, Rekening>
        {
            { "001", new Rekening("001", "Saiful", 10000000m) },
            { "002", new Rekening("002", "Siti", 60000000m) }
        };

        while (true)
        {
            Console.WriteLine("\n=== Menu Utama Layanan Bank Pelita ===");
            Console.WriteLine("1. Lihat Data Nasabah");
            Console.WriteLine("2. Setor Dana");
            Console.WriteLine("3. Tarik Dana");
            Console.WriteLine("4. Transfer Dana");
            Console.WriteLine("5. Keluar");
            Console.Write("Pilih menu (Ketik angka pada pilihan): ");
            string Pilihan = Console.ReadLine();

            switch (Pilihan)
            {
                case "1":
                    foreach (var rekening in Data_Rekening.Values)
                    {
                        rekening.Data_Nasabah();
                    }
                    break;

                case "2":
                    Console.Write("Masukkan nomor rekening: ");
                    string Norek_Setor = Console.ReadLine();
                    if (Data_Rekening.ContainsKey(Norek_Setor))
                    {
                        Console.Write("Masukkan jumlah setor: ");
                        decimal Jumlah_Setor = Convert.ToDecimal(Console.ReadLine());
                        Data_Rekening[Norek_Setor].Setor_Dana(Jumlah_Setor);
                    }
                    else
                    {
                        Console.WriteLine("Rekening tidak ditemukan.");
                    }
                    break;

                case "3":
                    Console.Write("Masukkan nomor rekening: ");
                    string Norek_Tarik = Console.ReadLine();
                    if (Data_Rekening.ContainsKey(Norek_Tarik))
                    {
                        Console.Write("Masukkan nominal yang ingin ditarik: ");
                        decimal Jumlah_Tarik = Convert.ToDecimal(Console.ReadLine());
                        Data_Rekening[Norek_Tarik].Tarik_Dana(Jumlah_Tarik);
                    }
                    else
                    {
                        Console.WriteLine("Rekening tidak ditemukan.");
                    }
                    break;

                case "4":
                    Console.Write("Masukkan nomor rekening pengirim: ");
                    string Dari = Console.ReadLine();
                    Console.Write("Masukkan nomor rekening penerima: ");
                    string Ke = Console.ReadLine();
                    if (Data_Rekening.ContainsKey(Dari) && Data_Rekening.ContainsKey(Ke))
                    {
                        Console.Write("Masukkan nominal transfer: ");
                        decimal Jumlah_Transfer = Convert.ToDecimal(Console.ReadLine());
                        Data_Rekening[Dari].Transfer_Dana(Data_Rekening[Ke], Jumlah_Transfer);
                    }
                    else
                    {
                        Console.WriteLine("Rekening pengirim atau penerima tidak ditemukan.");
                    }
                    break;

                case "5":
                    Console.WriteLine("Terima kasih telah menggunakan layanan Kami.");
                    return;

                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
    }
}