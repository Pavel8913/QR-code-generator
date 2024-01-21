using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.IO;

namespace qr_code
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            {
                try
                {
                        string qrtext = textBox1.Text; //считываем текст из TextBox'a            
                    QRCodeEncoder encoder = new QRCodeEncoder(); //создаём новую "генерацию кода"           
                    Bitmap qrcode = encoder.Encode(qrtext, Encoding.UTF8); // кодируем слово, полученное из TextBox'a (qrtext) в переменную qrcode. класса Bitmap(класс, который используется для работы с изображениями)            
                    pictureBox1.Image = qrcode as Image; // pictureBox выводит qrcode как изображение.
                }
                catch
                {
                    MessageBox.Show("Ошибка создания QR кода.", "Ошибка!");
                }
                    
            }
            else
            {

                MessageBox.Show("Сначало напишите, что хотите закодировать в QR код.", "Ошибка!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image!=null)
            {
                SaveFileDialog save = new SaveFileDialog(); // save будет запрашивать у пользователя место, в которое он захочет сохранить файл. 
                save.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp"; //создаём фильтр, который определяет, в каких форматах мы сможем сохранить нашу информацию. В данном случае выбираем форматы изображений. Записывается так: "название_формата_в обозревателе|*.расширение_формата")
                if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK) //если пользователь нажимает в обозревателе кнопку "Сохранить".
                {
                    pictureBox1.Image.Save(save.FileName); //изображение из pictureBox'a сохраняется под именем, которое введёт пользователь
                }

            }
            else
            {
                MessageBox.Show("Сначало создайте QR код.", "Ошибка!");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog(); //  load будет запрашивать у пользователя место, из которого он хочет загрузить файл.
            if (load.ShowDialog() == System.Windows.Forms.DialogResult.OK) // //если пользователь нажимает в обозревателе кнопку "Открыть".
            {
                pictureBox1.ImageLocation = load.FileName; // в pictureBox'e открывается файл, который был по пути, запрошенном пользователем.
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image!=null)
            {
                try
                {
                        QRCodeDecoder decoder = new QRCodeDecoder(); // создаём "раскодирование изображения"
                        string s = decoder.decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap), Encoding.UTF8);
                        MessageBox.Show(decoder.decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap), Encoding.UTF8),"Сообщение:"); //в MessageBox'e программа запишет раскодированное сообщение с изображения, которое предоврительно будет переведено из pictureBox'a в класс Bitmap, чтобы мы смогли с этим изображением работать. 
                }
                catch
                {
                    MessageBox.Show("Размер распознаваемого QR кода не подходящий. Распозноваемый размер: 4.", "Ошибка!");
                }
            
            }
            else
            {
                MessageBox.Show("Сначало загрузите QR код.", "Ошибка!");
            }
            
        }

        
    }
}
