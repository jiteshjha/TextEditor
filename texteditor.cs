using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class MenuDialog : Form {
  TextBox text = new TextBox();

  public MenuDialog() {
    Size = new Size(500,200);

    text.Size = new Size(490,190);
    text.Multiline = true;
    text.ScrollBars = ScrollBars.Both;
    text.WordWrap = false;
    text.Location = new Point(5,5);

    MenuItem fileMenu = new MenuItem("File");
    MenuItem open = new MenuItem("Open");
    open.Shortcut = Shortcut.CtrlO;
    MenuItem save = new MenuItem("Save");
    save.Shortcut = Shortcut.CtrlS;
    fileMenu.MenuItems.Add(open);
    fileMenu.MenuItems.Add(save);

    MenuItem formatMenu = new MenuItem("Format");
    MenuItem font = new MenuItem("Font");
    font.Shortcut = Shortcut.CtrlF;
    formatMenu.MenuItems.Add(font);
     
    MainMenu bar = new MainMenu();
    Menu = bar;
    bar.MenuItems.Add(fileMenu);
    bar.MenuItems.Add(formatMenu);

    Controls.Add(text);

    open.Click += new EventHandler(Open_Click);
    save.Click += new EventHandler(Save_Click);
    font.Click += new EventHandler(Font_Click); 
  }
  
  protected void Open_Click(Object sender, EventArgs e) {
    OpenFileDialog o = new OpenFileDialog();
    if(o.ShowDialog() == DialogResult.OK) {
      Stream file = o.OpenFile();
      StreamReader reader = new StreamReader(file);
      char[] data = new char[file.Length];
      reader.ReadBlock(data,0,(int)file.Length);
      text.Text = new String(data);  
      reader.Close();
    }
  }

  protected void Save_Click(Object sender, EventArgs e) {
    SaveFileDialog s = new SaveFileDialog();
    if(s.ShowDialog() == DialogResult.OK) {
      StreamWriter writer = new StreamWriter(s.OpenFile());
      writer.Write(text.Text);
      writer.Close();
    }
  }
  protected void Font_Click(Object sender, EventArgs e) {
    FontDialog f = new FontDialog();
    if(f.ShowDialog() == DialogResult.OK) 
      text.Font = f.Font;
  }

  public static void Main() {
    Application.Run(new MenuDialog());
  }
}
