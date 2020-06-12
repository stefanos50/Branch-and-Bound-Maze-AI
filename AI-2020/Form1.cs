using nu.xom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_2020
{
    public partial class Form1 : Form
    {
        public List<string> paths = new List<string>();
        public int[,] maze = new int[,]
        {
            { 1, 0, 1, 1, 1 },
            { 1, 0, 0, 0, 1 },
            { 1, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1 },
        };
        public string e_node, e_node_path;
        public int e_node_i, e_node_j;
        Stack nodes = new Stack();
        public List<string> childs = new List<string>();
        public List<string> closeset = new List<string>();
        public int boundd = Int32.MaxValue;
        public string solution;
        public Form1()
        {
            InitializeComponent();
            print_array();
            nodes.Push("(0,1)");
        }

        private void print_solution(string sol)
        {
            Button[,] nodes_maze = new Button[,]
            {
                { circularButton1, circularButton7, circularButton8, circularButton9, circularButton10 },
                { circularButton2, circularButton14, circularButton13, circularButton12, circularButton11},
                { circularButton3, circularButton18, circularButton17, circularButton16, circularButton15},
                { circularButton4, circularButton22, circularButton21, circularButton20, circularButton19 },
                { circularButton5, circularButton26, circularButton25, circularButton24, circularButton23},
                { circularButton6,circularButton30, circularButton29, circularButton28, circularButton27},
            };
            string[] nds = sol.Split(' ');
            int k = 0;
            for(int i=0;i<nds.Length;i++)
            {
                nodes_maze[Int32.Parse((nds[i][1].ToString()).ToString()), Int32.Parse((nds[i][3].ToString()).ToString())].BackColor = Color.Red;
                if (i == 0)
                    label6.Text += nds[i].ToString();
                else
                    label6.Text += ("->" + nds[i].ToString());
            }
            label6.Visible = true;
            label5.Visible = true;
            pictureBox60.Visible = true;
        }

        private void start_again()
        {
            maze = new int[,]
            {
                { 1, 0, 1, 1, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1 },
            };
            nodes.Clear();
            nodes.Push("(0,1)");
            nodes_update();
            label6.Visible = false;
            label5.Visible = false;
            pictureBox60.Visible = false;
            label6.Text = "";
            print_array();
        }
        private void print_array()
        {
            string matrixString = "";
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    matrixString += maze[i, j].ToString();
                    matrixString += "  ";
                }

                matrixString += Environment.NewLine;
            }
            richTextBox1.Text = matrixString;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "Next Step")
            {
                string stack_str;
                if (nodes.Count > 0)
                {
                    stack_str = stack_to_string();
                }
                else
                {
                    stack_str = "";
                }


                e_node_path = nodes.Pop().ToString();
                string[] nds = e_node_path.Split(' ');
                e_node = nds[nds.Length - 1];
                e_node_i = Int32.Parse(e_node[1].ToString());
                e_node_j = Int32.Parse(e_node[3].ToString());
                for (int j = 0; j < nds.Length; j++)
                {
                    maze[Int32.Parse(nds[j][1].ToString()), Int32.Parse(nds[j][3].ToString())] = 1;
                }
                print_array();

                try
                {
                    if (maze[e_node_i, e_node_j - 1] == 0)
                    {
                        nodes.Push(e_node_path + " (" + e_node_i.ToString() + "," + (e_node_j - 1).ToString() + ")");
                        childs.Add(e_node_path + " (" + e_node_i.ToString() + "," + (e_node_j - 1).ToString() + ")");
                    }
                }
                catch (Exception e3)
                {
                }

                try
                {
                    if (maze[e_node_i - 1, e_node_j] == 0)
                    {
                        nodes.Push(e_node_path + " (" + (e_node_i - 1).ToString() + "," + e_node_j.ToString() + ")");
                        childs.Add(e_node_path + " (" + (e_node_i - 1).ToString() + "," + e_node_j.ToString() + ")");
                    }
                }
                catch (Exception e4)
                {
                }

                try
                {
                    if (maze[e_node_i, e_node_j + 1] == 0)
                    {
                        nodes.Push(e_node_path + " (" + e_node_i.ToString() + "," + (e_node_j + 1).ToString() + ")");
                        childs.Add(e_node_path + " (" + e_node_i.ToString() + "," + (e_node_j + 1).ToString() + ")");
                    }
                }
                catch (Exception e1)
                {
                }

                try
                {
                    if (maze[e_node_i + 1, e_node_j] == 0)
                    {
                        nodes.Push(e_node_path + " (" + (e_node_i + 1).ToString() + "," + e_node_j.ToString() + ")");
                        childs.Add(e_node_path + " (" + (e_node_i + 1).ToString() + "," + e_node_j.ToString() + ")");
                    }
                }
                catch (Exception e2)
                {
                }

                if (e_node == "(0,1)")
                {
                    this.dataGridView1.Rows.Add("(0,1)", "∅", "(0,1)", "+∞", list_to_string(childs));
                }else if (e_node == "(5,1)")
                {
                    maze = new int[,]
                    {
                        { 1, 0, 1, 1, 1 },
                        { 1, 0, 0, 0, 1 },
                        { 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 0, 1 },
                        { 1, 0, 0, 0, 1 },
                        { 1, 0, 1, 1, 1 },
                    };
                    if (nds.Length < boundd)
                    {
                        boundd = nds.Length;
                        solution = e_node_path;
                        this.dataGridView1.Rows.Add(stack_str, list_to_string(closeset), e_node_path, boundd, "ΛΥΣΗ");
                    }
                }else if(nds.Length > boundd)
                {
                    maze = new int[,]
                    {
                        { 1, 0, 1, 1, 1 },
                        { 1, 0, 0, 0, 1 },
                        { 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 0, 1 },
                        { 1, 0, 0, 0, 1 },
                        { 1, 0, 1, 1, 1 },
                    };
                    this.dataGridView1.Rows.Add(stack_str, list_to_string(closeset), e_node_path, boundd, "ΚΛΑΔΕΜΑ");
                    nodes.Pop();
                }
                else
                {
                    if(boundd == Int32.MaxValue)
                    {
                        this.dataGridView1.Rows.Add(stack_str, list_to_string(closeset), e_node_path, "+∞", list_to_string(childs));
                    }
                    else
                    {
                        this.dataGridView1.Rows.Add(stack_str, list_to_string(closeset), e_node_path, boundd, list_to_string(childs));
                    }
                }
                closeset.Add(e_node_path);
                childs.Clear();
                nodes_update();
                print_array();
                if (nodes.Count == 0)
                {
                    this.dataGridView1.Rows.Add("∅", list_to_string(closeset), "∅", boundd, "∅");
                    print_solution(solution);
                    button1.Text = "Start Again";
                }
            }
            else
            {
                start_again();
                button1.Text = "Next Step";
            }
        }
        public string stack_to_string()
        {
            string res = "";
            foreach (string i in nodes)
            {
                res += (i + "   ");
            }
            return res;
        }
        public string list_to_string(List<string> ls)
        {
            string res = "";
            foreach (string i in ls)
            {
                res += (i + "   ");
            }
            return res;
        }
        private void nodes_update()
        {
            Button[,] nodes_maze = new Button[,]
            {
                { circularButton1, circularButton7, circularButton8, circularButton9, circularButton10 },
                { circularButton2, circularButton14, circularButton13, circularButton12, circularButton11},
                { circularButton3, circularButton18, circularButton17, circularButton16, circularButton15},
                { circularButton4, circularButton22, circularButton21, circularButton20, circularButton19 },
                { circularButton5, circularButton26, circularButton25, circularButton24, circularButton23},
                { circularButton6,circularButton30, circularButton29, circularButton28, circularButton27},
            };

            int rowLength = maze.GetLength(0);
            int colLength = maze.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (maze[i,j] == 1)
                    {
                        nodes_maze[i, j].BackColor = Color.RoyalBlue;
                    }
                    else
                    {
                        nodes_maze[i, j].BackColor = Color.LightSkyBlue;
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
