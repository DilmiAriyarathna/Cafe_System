Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form23
    ''Dim rs As ADODB.Recordset

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"

    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"

    Dim year, month As String
    Dim i, x, store_id, j, q, qty, qtytot As Integer
    Dim User_Name As String

    Dim unit_cost As Double

    Dim dis1 As Double
    Dim dis1_amt As Double

    Dim vat As Double

    Dim total, tot As Double

    Dim table As New DataTable("Table")

    Dim tot_amt, dis, dis_amt, net_amt, vat_amt, grd_tot As Double

    ''
    '///po grn details variables'//

    ''//''
    Private Sub Form23_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Form1.setname = "") Then
            Label5.Text = ""
        Else
            Label5.Text = Form1.setname
        End If
        ''
        If (Form1.setempname = "") Then
            User_Name = ""
        Else
            User_Name = Form1.setempname
        End If
        ''
        Timer1.Start()
        ''
        year = Date.Now.Year.ToString()
        month = Date.Now.Month.ToString()
        Label11.Text = year.Substring(year.Length - 2, 2)
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(GRN)GRN from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label12.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''''
        Dim i = Convert.ToInt32(Label12.Text)
        i += 1
        Label13.Text = i.ToString()
        ''''
        Label6.Text = "GRN:" + month + "/" + Label11.Text + "/" + "0" + Label13.Text
        ''
        Me.KeyPreview = True
        ''
        fillcombo()
        ''
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name from Item", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''
        crgrid()
        ''
        grd()
        ''
        DateTimePicker2.Value = DateTimePicker2.Value.AddMonths(7)
        ''
        BunifuCustomDataGrid2.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        BunifuCustomDataGrid2.DefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        BunifuCustomDataGrid3.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 6)
        ''
        BunifuCustomDataGrid3.DefaultCellStyle.Font = New Font("Verdena", 6)
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 100
        BunifuCustomDataGrid1.Columns(1).Width = 335

        BunifuCustomDataGrid2.Columns(1).Width = 250
    End Sub
    Private Sub insert1()
        For i = 0 To BunifuCustomDataGrid2.RowCount - 1
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Stock( Code,Discription,Dr_qty,Cr_qty,Unit_Cost,R_Price,P_Price,S_Price, " &
                  "Total,FOC,VAT,Supplier,Store,GRN_No,Login,Cashier,PCK,No_pks,GRN_Invoice,Date,Flag,Type,TXT) " &
                  "values( " &
           "  @Code,@Discription,@Dr_qty,@Cr_qty,@Unit_Cost,@R_Price,@P_Price,@S_Price,@Total,@FOC, " &
           "@VAT,@Supplier,@Store,@GRN_No,@Login,@Cashier,@PCK,@No_pks,@GRN_Invoice,@Date,@Flag,@Type,@TXT)"
            cmd = New SqlCommand(SQL, conn)
            With cmd
                '  .Parameters.AddWithValue("@ID", Label13.Text)

                .Parameters.AddWithValue("@Code", BunifuCustomDataGrid2.Rows(i).Cells(0).Value)
                .Parameters.AddWithValue("@Discription", BunifuCustomDataGrid2.Rows(i).Cells(1).Value)
                .Parameters.AddWithValue("@Dr_qty", BunifuCustomDataGrid2.Rows(i).Cells(2).Value + BunifuCustomDataGrid2.Rows(i).Cells(8).Value)
                .Parameters.AddWithValue("@Cr_qty", 0)
                .Parameters.AddWithValue("@Unit_Cost", BunifuCustomDataGrid2.Rows(i).Cells(3).Value)

                .Parameters.AddWithValue("@R_Price", BunifuCustomDataGrid2.Rows(i).Cells(12).Value)
                .Parameters.AddWithValue("@P_Price", BunifuCustomDataGrid2.Rows(i).Cells(13).Value)
                .Parameters.AddWithValue("@S_Price", BunifuCustomDataGrid2.Rows(i).Cells(14).Value)

                .Parameters.AddWithValue("@Total", BunifuCustomDataGrid2.Rows(i).Cells(4).Value)
                .Parameters.AddWithValue("@FOC", BunifuCustomDataGrid2.Rows(i).Cells(8).Value)
                .Parameters.AddWithValue("@VAT", BunifuCustomDataGrid2.Rows(i).Cells(7).Value)
                .Parameters.AddWithValue("@Supplier", BunifuCustomDataGrid2.Rows(i).Cells(11).Value)
                .Parameters.AddWithValue("@Store", ComboBox1.Text)
                .Parameters.AddWithValue("@GRN_No", Label6.Text)

                .Parameters.AddWithValue("@Login", Label5.Text)
                .Parameters.AddWithValue("@Cashier", User_Name)
                .Parameters.AddWithValue("@PCK", BunifuCustomDataGrid2.Rows(i).Cells(15).Value)
                .Parameters.AddWithValue("@No_pks", BunifuCustomDataGrid2.Rows(i).Cells(16).Value)

                .Parameters.AddWithValue("@GRN_Invoice", TextBox3.Text)

                .Parameters.AddWithValue("@Date", d.ToString(formata))
                .Parameters.AddWithValue("@Flag", "1")
                .Parameters.AddWithValue("@Type", "GRN")
                .Parameters.AddWithValue("@TXT", "GRN:" + ComboBox1.Text)

                .ExecuteNonQuery()
            End With
            conn.Close()
        Next
        '   MsgBox("Done !")
    End Sub
    Private Sub insert2()
        For i = 0 To BunifuCustomDataGrid2.RowCount - 1
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Stock( Code,Discription,Dr_qty,Cr_qty,Unit_Cost,R_Price,P_Price,S_Price, " &
                  "Total,FOC,VAT,Supplier,Store,GRN_No,Login,Cashier,PCK,No_pks,GRN_Invoice,Date,Flag,Type,TXT) " &
                  "values( " &
           "  @Code,@Discription,@Dr_qty,@Cr_qty,@Unit_Cost,@R_Price,@P_Price,@S_Price,@Total,@FOC, " &
           "@VAT,@Supplier,@Store,@GRN_No,@Login,@Cashier,@PCK,@No_pks,@GRN_Invoice,@Date,@Flag,@Type,@TXT)"
            cmd = New SqlCommand(SQL, conn)
            With cmd
                '  .Parameters.AddWithValue("@ID", Label13.Text)

                .Parameters.AddWithValue("@Code", BunifuCustomDataGrid2.Rows(i).Cells(0).Value)
                .Parameters.AddWithValue("@Discription", BunifuCustomDataGrid2.Rows(i).Cells(1).Value)
                .Parameters.AddWithValue("@Dr_qty", 0)
                .Parameters.AddWithValue("@Cr_qty", BunifuCustomDataGrid2.Rows(i).Cells(2).Value + BunifuCustomDataGrid2.Rows(i).Cells(8).Value)
                .Parameters.AddWithValue("@Unit_Cost", BunifuCustomDataGrid2.Rows(i).Cells(3).Value)

                .Parameters.AddWithValue("@R_Price", BunifuCustomDataGrid2.Rows(i).Cells(12).Value)
                .Parameters.AddWithValue("@P_Price", BunifuCustomDataGrid2.Rows(i).Cells(13).Value)
                .Parameters.AddWithValue("@S_Price", BunifuCustomDataGrid2.Rows(i).Cells(14).Value)

                .Parameters.AddWithValue("@Total", BunifuCustomDataGrid2.Rows(i).Cells(4).Value)
                .Parameters.AddWithValue("@FOC", BunifuCustomDataGrid2.Rows(i).Cells(8).Value)
                .Parameters.AddWithValue("@VAT", BunifuCustomDataGrid2.Rows(i).Cells(7).Value)
                .Parameters.AddWithValue("@Supplier", BunifuCustomDataGrid2.Rows(i).Cells(11).Value)
                .Parameters.AddWithValue("@Store", ComboBox1.Text)
                .Parameters.AddWithValue("@GRN_No", Label6.Text)

                .Parameters.AddWithValue("@Login", Label5.Text)
                .Parameters.AddWithValue("@Cashier", User_Name)
                .Parameters.AddWithValue("@PCK", BunifuCustomDataGrid2.Rows(i).Cells(15).Value)
                .Parameters.AddWithValue("@No_pks", BunifuCustomDataGrid2.Rows(i).Cells(16).Value)

                .Parameters.AddWithValue("@GRN_Invoice", TextBox3.Text)

                .Parameters.AddWithValue("@Date", d.ToString(formata))
                .Parameters.AddWithValue("@Flag", "1")
                .Parameters.AddWithValue("@Type", "GRN")
                .Parameters.AddWithValue("@TXT", "GRN:" + ComboBox1.Text)

                .ExecuteNonQuery()
            End With
            conn.Close()
        Next

        '''''
        For j = 0 To BunifuCustomDataGrid2.RowCount - 1
            conn.Open()
            SQL = "insert into Stock_Perform(Date,time,Code,Discription,Qty,Unit_Cost,Total,FOC,Supplier, " &
            "Store,GRN_No,GRN_Id,Login,Cashier,PCK,No_pks,GRN_Invoice,PO_num,Flag,Type,TXT,PO_Date, " &
            "GRN_Date,Total_Amt,Discount,Dis_Amt,Net_Amt,VAT,Grand,PO_qty,bal) values ( " &
            "@Date,@time,@Code,@Discription,@Qty,@Unit_Cost,@Total,@FOC,@Supplier,@Store, " &
            "@GRN_No,@GRN_Id,@Login,@Cashier,@PCK,@No_pks,@GRN_Invoice,@PO_num,@Flag,@Type, " &
            "@TXT,@PO_Date,@GRN_Date,@Total_Amt,@Discount,@Dis_Amt,@Net_Amt,@VAT,@Grand,@PO_qty,@bal)"
            cmd4 = New SqlCommand(SQL, conn)
            With cmd4
                .Parameters.AddWithValue("@Date", d.ToString(formata))
                .Parameters.AddWithValue("@Time", t.ToString(formatb))

                .Parameters.AddWithValue("@Code", BunifuCustomDataGrid2.Rows(j).Cells(0).Value)
                .Parameters.AddWithValue("@Discription", BunifuCustomDataGrid2.Rows(j).Cells(1).Value)
                .Parameters.AddWithValue("@Qty", BunifuCustomDataGrid2.Rows(j).Cells(2).Value + BunifuCustomDataGrid2.Rows(j).Cells(8).Value)
                .Parameters.AddWithValue("@Unit_Cost", BunifuCustomDataGrid2.Rows(j).Cells(3).Value)
                .Parameters.AddWithValue("@Total", BunifuCustomDataGrid2.Rows(j).Cells(4).Value)
                .Parameters.AddWithValue("@FOC", BunifuCustomDataGrid2.Rows(j).Cells(8).Value)
                .Parameters.AddWithValue("@Supplier", BunifuCustomDataGrid2.Rows(j).Cells(11).Value)
                .Parameters.AddWithValue("@Store", ComboBox1.Text)
                .Parameters.AddWithValue("@GRN_No", Label6.Text)

                .Parameters.AddWithValue("@GRN_Id", Label13.Text)
                .Parameters.AddWithValue("@Login", Label5.Text)
                .Parameters.AddWithValue("@Cashier", User_Name)

                .Parameters.AddWithValue("@PCK", BunifuCustomDataGrid2.Rows(j).Cells(15).Value)
                .Parameters.AddWithValue("@No_pks", BunifuCustomDataGrid2.Rows(j).Cells(16).Value)
                .Parameters.AddWithValue("@GRN_Invoice", TextBox3.Text)

                .Parameters.AddWithValue("@PO_num", TextBox34.Text)
                .Parameters.AddWithValue("@Flag", "1")
                .Parameters.AddWithValue("@Type", "GRN")
                .Parameters.AddWithValue("@TXT", "GRN:" + ComboBox1.Text)

                .Parameters.AddWithValue("@PO_Date", DateTimePicker1.Value)
                .Parameters.AddWithValue("@GRN_Date", DateTimePicker1.Value)

                .Parameters.AddWithValue("@Total_Amt", TextBox23.Text)
                .Parameters.AddWithValue("@Discount", TextBox24.Text)
                .Parameters.AddWithValue("@Dis_Amt", TextBox25.Text)
                .Parameters.AddWithValue("@Net_Amt", TextBox26.Text)
                .Parameters.AddWithValue("@VAT", TextBox27.Text)
                .Parameters.AddWithValue("@Grand", TextBox28.Text)

                .Parameters.AddWithValue("@PO_qty", BunifuCustomDataGrid2.Rows(j).Cells(2).Value)
                .Parameters.AddWithValue("@bal", "0")

                .ExecuteNonQuery()
            End With
            conn.Close()
        Next
    End Sub
    Private Sub stk_qty()
        For q = 0 To BunifuCustomDataGrid2.RowCount - 1
            conn = GetConnect()
            '  conn.Open()
            SQL = "select * from Stock_Qty where Code='" + BunifuCustomDataGrid2.Rows(q).Cells(0).Value + "'"
            cmd1 = New SqlCommand(SQL, conn)
            dt = New DataTable
            da = New SqlDataAdapter(cmd1)
            da.Fill(dt)
            x = Convert.ToInt32(dt.Rows.Count.ToString())
            If x > 0 Then
                conn.Open()
                SQL = "select * from Stock_Qty where Code='" + BunifuCustomDataGrid2.Rows(q).Cells(0).Value + "'"
                cmd = New SqlCommand(SQL, conn)
                dr = cmd.ExecuteReader
                While (dr.Read())
                    qty = dr.GetValue(1).ToString
                End While
                conn.Close()
                ''
                qtytot = qty + BunifuCustomDataGrid2.Rows(q).Cells(2).Value + BunifuCustomDataGrid2.Rows(q).Cells(8).Value
                ''
                conn.Open()
                SQL = "Update Stock_Qty set QTY= @QTY,Final_Cost=@Final_Cost where Code='" + BunifuCustomDataGrid2.Rows(q).Cells(0).Value + "'"
                cmdq = New SqlCommand(SQL, conn)
                With cmdq.Parameters
                    .AddWithValue("@Code", BunifuCustomDataGrid2.Rows(q).Cells(0).Value)
                    .AddWithValue("@QTY", qtytot)
                    .AddWithValue("@Final_Cost", BunifuCustomDataGrid2.Rows(q).Cells(3).Value)
                End With
                cmdq.ExecuteNonQuery()
                conn.Close()
            Else
                conn.Open()
                SQL = "insert into Stock_Qty(Code,QTY,Final_Cost) values(@Code,@QTY,@Final_Cost)"
                cmdq = New SqlCommand(SQL, conn)
                With cmdq.Parameters
                    .AddWithValue("@Code", BunifuCustomDataGrid2.Rows(q).Cells(0).Value)
                    .AddWithValue("@QTY", BunifuCustomDataGrid2.Rows(q).Cells(2).Value + BunifuCustomDataGrid2.Rows(q).Cells(8).Value)
                    .AddWithValue("@Final_Cost", BunifuCustomDataGrid2.Rows(q).Cells(3).Value)
                End With
                cmdq.ExecuteNonQuery()
                conn.Close()
            End If
            'conn.Close()
        Next
    End Sub
    Private Sub ADD()
        For i = 0 To BunifuCustomDataGrid2.RowCount - 1
            conn = GetConnect()
            conn.Open()
            SQL = "insert into GRN_Details_tmp(GRN_No,Id,Grn_inv,Date,Time,Name,Supplier,Supp_Code,Login, " &
            "Store,I_Code,Discription,QTY,U_cost,total,disamt,dis,FOC,P_Price,R_Price,S_Price,W_Price,VAT, " &
            "exp_date,GRN_date,pack_Qty,PR_price,NOI,Total_Amt,Discount,Dis_Amt,Net_Amt, " &
            "VAT_amt,Grand_Tot,PO_num,flag) values ( " &
            "@GRN_No,@Id,@Grn_inv,@Date,@Time,@Name,@Supplier,@Supp_Code,@Login,@Store,@I_Code,@Discription, " &
            "@QTY,@U_cost,@total,@disamt,@dis,@FOC,@P_Price,@R_Price,@S_Price,@W_Price,@VAT,@exp_date, " &
            "@GRN_date,@pack_Qty,@PR_price,@NOI,@Total_Amt,@Discount,@Dis_Amt,@Net_Amt, " &
            "@VAT_amt,@Grand_Tot,@PO_num,@flag)"
            cmd = New SqlCommand(SQL, conn)
            With cmd
                .Parameters.AddWithValue("@GRN_No", Label6.Text)
                .Parameters.AddWithValue("@Id", Label13.Text)
                .Parameters.AddWithValue("@Grn_inv", TextBox3.Text)
                .Parameters.AddWithValue("@Date", d.ToString(formata))
                .Parameters.AddWithValue("@Time", t.ToString(formatb))
                .Parameters.AddWithValue("@Name", User_Name)
                .Parameters.AddWithValue("@Supplier", BunifuCustomDataGrid2.Rows(i).Cells(10).Value)
                .Parameters.AddWithValue("@Supp_Code", BunifuCustomDataGrid2.Rows(i).Cells(11).Value)
                .Parameters.AddWithValue("@Login", Label5.Text)
                .Parameters.AddWithValue("@Store", ComboBox1.Text)
                .Parameters.AddWithValue("@I_Code", BunifuCustomDataGrid2.Rows(i).Cells(0).Value)
                .Parameters.AddWithValue("@Discription", BunifuCustomDataGrid2.Rows(i).Cells(1).Value)
                .Parameters.AddWithValue("@QTY", BunifuCustomDataGrid2.Rows(i).Cells(2).Value + BunifuCustomDataGrid2.Rows(i).Cells(8).Value)
                .Parameters.AddWithValue("@U_cost", BunifuCustomDataGrid2.Rows(i).Cells(3).Value)
                .Parameters.AddWithValue("@total", BunifuCustomDataGrid2.Rows(i).Cells(4).Value)
                .Parameters.AddWithValue("@disamt", BunifuCustomDataGrid2.Rows(i).Cells(6).Value)
                .Parameters.AddWithValue("@dis", BunifuCustomDataGrid2.Rows(i).Cells(5).Value)
                .Parameters.AddWithValue("@FOC", BunifuCustomDataGrid2.Rows(i).Cells(8).Value)
                .Parameters.AddWithValue("@R_Price", BunifuCustomDataGrid2.Rows(i).Cells(12).Value)
                .Parameters.AddWithValue("@P_Price", BunifuCustomDataGrid2.Rows(i).Cells(13).Value)
                .Parameters.AddWithValue("@S_Price", BunifuCustomDataGrid2.Rows(i).Cells(14).Value)
                .Parameters.AddWithValue("@W_Price", BunifuCustomDataGrid2.Rows(i).Cells(17).Value)
                .Parameters.AddWithValue("@VAT", BunifuCustomDataGrid2.Rows(i).Cells(7).Value)
                .Parameters.AddWithValue("@exp_date", DateTimePicker2.Value)
                .Parameters.AddWithValue("@GRN_date", DateTimePicker1.Value)
                .Parameters.AddWithValue("@pack_Qty", BunifuCustomDataGrid2.Rows(i).Cells(15).Value)
                .Parameters.AddWithValue("@PR_price", BunifuCustomDataGrid2.Rows(i).Cells(16).Value)
                .Parameters.AddWithValue("@NOI", TextBox5.Text)
                .Parameters.AddWithValue("@Total_Amt", TextBox23.Text)
                .Parameters.AddWithValue("@Discount", TextBox24.Text)
                .Parameters.AddWithValue("@Dis_Amt", TextBox25.Text)
                .Parameters.AddWithValue("@Net_Amt", TextBox26.Text)
                .Parameters.AddWithValue("@VAT_amt", TextBox27.Text)
                .Parameters.AddWithValue("@Grand_Tot", TextBox28.Text)
                .Parameters.AddWithValue("@PO_num", TextBox34.Text)
                .Parameters.AddWithValue("@flag", "1")
                .ExecuteNonQuery()
            End With
            conn.Close()
        Next
    End Sub
    Private Sub crgrid()
        table.Columns.Add("Code", Type.GetType("System.String"))
        table.Columns.Add("Discription", Type.GetType("System.String"))
        table.Columns.Add("QTY", Type.GetType("System.Int32"))
        table.Columns.Add("Unit_Cost", Type.GetType("System.String"))
        table.Columns.Add("Total", Type.GetType("System.Double"))
        table.Columns.Add("Discount", Type.GetType("System.String"))
        table.Columns.Add("Discount_Amt", Type.GetType("System.String"))
        table.Columns.Add("VAT", Type.GetType("System.String"))
        table.Columns.Add("FOC", Type.GetType("System.Int32"))
        table.Columns.Add("Net_Total", Type.GetType("System.String"))
        table.Columns.Add("Supplier", Type.GetType("System.String"))
        table.Columns.Add("Supplier_Code", Type.GetType("System.String"))
        ''table.Columns.Add("Store", Type.GetType("System.String"))
        ''table.Columns.Add("GRN", Type.GetType("System.String"))
        ''table.Columns.Add("Inv", Type.GetType("System.String"))

        table.Columns.Add("R_Price", Type.GetType("System.String")) '15
        table.Columns.Add("P_Price", Type.GetType("System.String")) '16
        table.Columns.Add("S_Price", Type.GetType("System.String")) '17
        table.Columns.Add("PCK", Type.GetType("System.String"))     '18
        table.Columns.Add("No_pks", Type.GetType("System.String"))  '19
        table.Columns.Add("W_Price", Type.GetType("System.String"))  '20

        ''table.Columns.Add("Exp_date", Type.GetType("System.String"))  '21
        ''table.Columns.Add("GRN_date", Type.GetType("System.String"))  '22

        BunifuCustomDataGrid2.DataSource = table
        ' DataGridView1.DataSource = table
    End Sub
    Private Sub Additem()
        table.Rows.Add(TextBox21.Text, TextBox22.Text, TextBox6.Text, textBox8.Text, total, textBox9.Text, textBox10.Text,
                      textBox12.Text, textBox7.Text, TextBox19.Text, TextBox1.Text, TextBox2.Text,
                textBox8.Text, textBox11.Text, TextBox20.Text, textBox15.Text, textBox14.Text,
                       TextBox4.Text)
        BunifuCustomDataGrid2.DataSource = table
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = DateTime.Now.ToString()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form13.TextBox6.Text = "5"
        Form13.Show()
    End Sub

    Private Sub Form23_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Form12.Show()
        End If
        ''
        If e.KeyCode = Keys.Escape Then
            If Panel3.Visible = False Then
                Panel3.Visible = True
            ElseIf Panel3.Visible = True Then
                Panel3.Visible = False
            End If
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            conn = GetConnect()
            conn.Open()
            SQL = "select * from Stores where Name ='" + ComboBox1.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                store_id = dr.GetValue(0).ToString
            End While
            conn.Close()
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not TextBox3.Text = "" And Not TextBox3.Text = "0" Then
                conn = GetConnect()
                conn.Open()
                SQL = "select * from GRN_Details where Grn_inv='" + TextBox3.Text + "'"
                cmd1 = New SqlCommand(SQL, conn)
                dt = New DataTable
                da = New SqlDataAdapter(cmd1)
                da.Fill(dt)
                x = Convert.ToInt32(dt.Rows.Count.ToString())
                If x > 0 Then
                    MsgBox("GRN Invoice Number Already Exist !")
                    TextBox3.Text = ""
                    TextBox3.Focus()
                    Exit Sub
                Else
                    TextBox22.Focus()
                    Panel3.Visible = True
                End If
                conn.Close()
            Else
                MsgBox("Invalid GRN Invoice Number !")
                TextBox3.Text = ""
                TextBox3.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs)
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name from Item where Name like '%" + TextBox22.Text + "%'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox17.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(0).Value
        End If
        Panel3.Visible = False
    End Sub
    Private Sub filldata()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name,Code, CAST(P_Price as numeric(17,2))P_Price, CAST(R_Price as numeric(17,2))R_Price, " &
        " CAST(S_Price as numeric(17,2))S_Price,Weight, " &
        "pack_Qty, CAST(PR_Price as numeric(17,2))PR_price, P_wht,CAST(W_Price as numeric(17,2))W_Price from Item where Code = '" + TextBox17.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        Using dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox22.Text = dr.GetValue(0).ToString  'name
                TextBox21.Text = dr.GetValue(1).ToString  'code
                '  TextBox30.Text = dr.GetValue(1).ToString
                textBox11.Text = dr.GetValue(2).ToString 'ppric
                textBox8.Text = dr.GetValue(3).ToString  'rpric
                TextBox18.Text = dr.GetValue(3).ToString 'rpric
                TextBox20.Text = dr.GetValue(4).ToString 'spric
                textBox13.Text = dr.GetValue(5).ToString 'wht
                textBox15.Text = dr.GetValue(6).ToString 'pckqty
                textBox14.Text = dr.GetValue(7).ToString 'prpric
                textBox16.Text = dr.GetValue(8).ToString 'pwht
                TextBox4.Text = dr.GetValue(9).ToString 'wpric
            End While
        End Using
        conn.Close()

    End Sub

    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        filldata()
        TextBox6.Focus()
    End Sub

    Private Sub panel2_Paint(sender As Object, e As PaintEventArgs) Handles panel2.Paint

    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not TextBox6.Text = "" Then
                textBox7.Focus()
            Else
                MsgBox("Invalid QTY !")
                TextBox6.Focus()
                Exit Sub
            End If

            conn = GetConnect()
            conn.Open()
            SQL = "select Total_qty,Convert(Date,Convert(varchar,GetDate(),101))Date from Stock_Summary where Code = '" + TextBox30.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            Using dr = cmd.ExecuteReader
                While (dr.Read())
                    TextBox31.Text = dr.GetValue(0).ToString
                    DateTimePicker3.Value = dr.GetValue(1).ToString
                    'TextBox30.Text = dr.GetValue(1).ToString
                    'textBox11.Text = dr.GetValue(2).ToString 'ppric
                End While
            End Using
        End If
    End Sub

    Private Sub textBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If textBox7.Text = "" Then
                textBox7.Text = "0"
            End If
            textBox8.Focus()
        End If
    End Sub

    Private Sub textBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox8.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not textBox8.Text = "" Then
                textBox11.Focus()
                TextBox20.Text = textBox8.Text
            Else
                MsgBox("Invalid Unit Cost !")
                textBox8.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub textBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox11.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not textBox8.Text = "" Then
                textBox13.Focus()
            Else
                MsgBox("Invalid Purchase Price !")
                textBox11.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub textBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox13.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            textBox15.Focus()
        End If
    End Sub

    Private Sub textBox15_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox15.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            textBox14.Focus()
        End If
    End Sub

    Private Sub textBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox14.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            textBox16.Focus()
        End If
    End Sub

    Private Sub textBox16_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox16.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            textBox9.Focus()
        End If
    End Sub

    Private Sub textBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox9.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not textBox9.Text = "" Then
                textBox10.Enabled = False
                textBox10.Text = "0"
                textBox12.Focus()
            Else
                textBox9.Text = "0"
                textBox9.Enabled = False
                textBox10.Focus()
            End If

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not TextBox6.Text = "" And Not textBox8.Text = "" Then    'And Not textBox9.Text = "" And Not textBox10.Text = " then" Then
            total = TextBox6.Text * textBox8.Text
            TextBox35.Text = total.ToString("#.00")
            If Not textBox9.Text = "" Then
                dis1 = ((textBox9.Text / 100) * textBox8.Text) * TextBox6.Text
            ElseIf textBox9.Text = "" Then
                dis1 = 0
            End If

            If Not textBox10.Text = "" Then
                dis1_amt = textBox10.Text * TextBox6.Text
            ElseIf textBox10.Text = "" Then
                dis1_amt = 0
            End If

            If Not textBox12.Text = "" Then
                vat = textBox12.Text * TextBox6.Text
            ElseIf textBox12.Text = "" Then
                vat = 0
            End If

            tot = total - dis1 - dis1_amt + vat
            TextBox19.Text = tot.ToString("#.00")

            tot_amt = tot_amt + tot
            TextBox23.Text = tot_amt.ToString("#.00")

            net_amt = net_amt + tot_amt
            TextBox26.Text = net_amt.ToString("#.00")
        End If
    End Sub

    Private Sub textBox12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox12.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If textBox12.Text = "" Then
                textBox12.Text = "0"
            End If
            Button3_Click(sender, e)
            DateTimePicker2.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox19.Focus()
        End If
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click

    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged

    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            DateTimePicker1.Focus()
        End If
    End Sub
    Private Sub clear()
        TextBox21.Text = ""
        TextBox22.Text = ""
        textBox11.Text = ""
        textBox8.Text = ""
        TextBox18.Text = ""
        TextBox20.Text = ""
        textBox13.Text = ""
        textBox15.Text = ""
        textBox14.Text = ""
        textBox16.Text = ""
        TextBox4.Text = ""
        TextBox6.Text = ""
        textBox9.Text = ""
        textBox10.Text = ""
        textBox12.Text = ""
        textBox7.Text = ""
        TextBox19.Text = ""

        total = 0
        dis1 = 0
        dis1_amt = 0
        tot = 0

        textBox9.Enabled = True
        textBox10.Enabled = True
    End Sub
    Private Sub TextBox19_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox19.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Additem()
            'ADD()
            clear()
            Panel3.Visible = True

            Dim cntr As Integer

            cntr = BunifuCustomDataGrid2.Rows.Count
            TextBox5.Text = cntr
        End If
    End Sub

    Private Sub TextBox24_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox24.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not TextBox24.Text = "" Then
                If TextBox25.Text = "" Then
                    dis = (TextBox24.Text / 100) * TextBox23.Text
                    net_amt = TextBox26.Text - dis
                    TextBox26.Text = net_amt.ToString("#.00")
                ElseIf Not TextBox25.Text = "" Then
                    MsgBox("Discount Already Add !")
                    TextBox24.Text = ""
                    Exit Sub
                End If
                TextBox27.Focus()
            ElseIf TextBox24.Text = "" Then
                dis = 0
                net_amt = TextBox26.Text - dis
                TextBox26.Text = net_amt.ToString("#.00")
                TextBox25.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox25_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox25.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not TextBox25.Text = "" Then
                If TextBox24.Text = "" Then
                    dis_amt = TextBox25.Text
                    net_amt = TextBox26.Text - dis_amt
                    TextBox26.Text = net_amt.ToString("#.00")
                ElseIf Not TextBox24.Text = "" Then
                    MsgBox("Discount Already Add !")
                    TextBox25.Text = ""
                    Exit Sub
                End If
            ElseIf TextBox24.Text = "" Then
                dis_amt = 0
                net_amt = TextBox26.Text - dis_amt
                TextBox26.Text = net_amt.ToString("#.00")
            End If
            TextBox27.Focus()
        End If
    End Sub

    Private Sub TextBox30_TextChanged(sender As Object, e As EventArgs) Handles TextBox30.TextChanged


    End Sub

    Private Sub TextBox21_TextChanged(sender As Object, e As EventArgs) Handles TextBox21.TextChanged
        TextBox30.Text = TextBox21.Text
    End Sub

    Private Sub TextBox27_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox27.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If TextBox27.Text = "" Then
                vat_amt = 0
                grd_tot = TextBox26.Text + vat_amt
                TextBox28.Text = grd_tot.ToString("#.00")
            ElseIf Not TextBox27.Text = "" Then
                vat_amt = TextBox27.Text
                grd_tot = TextBox26.Text + vat_amt
                TextBox28.Text = grd_tot.ToString("#.00")
            End If
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox28.Text = "" Then
            MsgBox("Invalid Grand Total !")
            Exit Sub
        End If

        conn = GetConnect()
        conn.Open()
        SQL = "truncate table GRN_Details_tmp"
        cmd5 = New SqlCommand(SQL, conn)
        cmd5.ExecuteNonQuery()
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [GRN] = [GRN] + 1"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''
        ADD()
        ''
        conn.Open()
        SQL = "insert into GRN_Details select * from GRN_Details_tmp where GRN_No = '" + Label6.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
        ''
        insert1()
        ''
        insert2()
        ''
        stk_qty()
        ''
        conn.Open()
        SQL = "update GRN_Details_tmp set Time = '" + t.ToString(formatb) + "' " &
            " where  GRN_No = '" + Label6.Text + "'"
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''''
        conn.Open()
        SQL = "update GRN_Details set Time = '" + t.ToString(formatb) + "'  " &
            " where  GRN_No = '" + Label6.Text + "'"
        cmd6 = New SqlCommand(SQL, conn)
        cmd6.ExecuteNonQuery()
        conn.Close()
        ''
        If Not TextBox34.Text = "" Then
            conn.Open()
            SQL = "update Purchase_Order set Flag = '10' , I_flag = '10' " &
                " where  PO_num = '" + TextBox34.Text + "'"
            cmdq = New SqlCommand(SQL, conn)
            cmdq.ExecuteNonQuery()
            conn.Close()
            ''
            conn.Open()
            SQL = "update Purchase_chk set Flag = 'G' " &
                " where  PO_Num = '" + TextBox34.Text + "'"
            cmdw = New SqlCommand(SQL, conn)
            cmdw.ExecuteNonQuery()
            conn.Close()
        End If
        MsgBox("Updated Successfully !")
    End Sub

    Private Sub Clr()
        Dim Form23 As New Form23
        Me.Hide()
        Form23.Show()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Clr()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form25.Show()
    End Sub

    Private Sub TextBox29_Click(sender As Object, e As EventArgs) Handles TextBox29.Click
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("SELECT PO_Num,PO_Date FROM [Cafe_SYS].[dbo].[Purchase_chk] where flag = 'C' GROUP BY PO_Num,PO_Date", conn)

        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid3.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid3.Columns(0).Width = 85
        BunifuCustomDataGrid3.Columns(1).Width = 75
    End Sub

    Private Sub BunifuCustomDataGrid3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid3.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox34.Text = BunifuCustomDataGrid3.Rows(e.RowIndex).Cells(0).Value
            TextBox29.Text = BunifuCustomDataGrid3.Rows(e.RowIndex).Cells(0).Value
        End If
    End Sub

    Private Sub TextBox34_TextChanged(sender As Object, e As EventArgs) Handles TextBox34.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("SELECT Code,Name,QTY,CAST(Unit_Cost as numeric(17,2))Unit_Cost,CAST(Total as numeric(17,2))Total,FOC,Supp_code,Supplier, " &
                                "CAST(R_Price as numeric(17,2))R_Price,CAST(P_Price as numeric(17,2))P_Price, " &
                                "CAST(S_Price as numeric(17,2))S_Price,CAST(W_Price as numeric(17,2))W_Price, " &
                                "pack_Qty,CAST(PR_price as numeric(17,2))PR_price FROM PO_GRN where PO_Num='" + TextBox34.Text + "'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid4.DataSource = dt
        conn.Close()
        ''
        'TextBox1.Text = ComboBox1.Text
        po_additem()
        ''
        po_details()
        ''
        po_totamt()
        ''
        ComboBox1.Focus()
        SendKeys.Send("{F4}")
    End Sub
    Private Sub po_additem()
        Dim dre As New System.Windows.Forms.DataGridViewRow
        For Each dre In Me.BunifuCustomDataGrid4.Rows
            table.Rows.Add(dre.Cells(0).Value, dre.Cells(1).Value, dre.Cells(2).Value, dre.Cells(3).Value, dre.Cells(4).Value,
                           "0", "0", "0", dre.Cells(5).Value, dre.Cells(4).Value, dre.Cells(7).Value, dre.Cells(6).Value, dre.Cells(8).Value,
                           dre.Cells(9).Value, dre.Cells(10).Value, dre.Cells(12).Value, dre.Cells(13).Value, dre.Cells(11).Value)
            BunifuCustomDataGrid2.DataSource = table
        Next
    End Sub

    Private Sub po_details()
        conn = GetConnect()
        conn.Open()
        SQL = "select Supp_code,Supplier from PO_GRN where PO_Num = '" + TextBox34.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        Using dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox2.Text = dr.GetValue(0).ToString  'code
                TextBox1.Text = dr.GetValue(1).ToString  'name
            End While
        End Using
        conn.Close()
    End Sub
    Private Sub po_totamt()
        Dim totnew As Double
        For g As Integer = 0 To BunifuCustomDataGrid4.RowCount - 1
            totnew += BunifuCustomDataGrid4.Rows(g).Cells(4).Value
        Next
        'If totnew = 0 Then
        '    MessageBox.Show("No Records Found")
        'End If
        TextBox23.Text = totnew.ToString("#.00")
        TextBox26.Text = totnew.ToString("#.00")
    End Sub

    Private Sub TextBox29_TextChanged(sender As Object, e As EventArgs) Handles TextBox29.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("SELECT PO_Num,PO_Date FROM [Cafe_SYS].[dbo].[Purchase_chk] where flag = 'C' and PO_Num Like '%" + TextBox29.Text + "%' GROUP BY PO_Num,PO_Date", conn)

        'da = New SqlDataAdapter("select PO_num,CAST(Date AS DATE)Date " &
        '                        " from PO_vw where Flag = '1' and PO_Num Like '%" + TextBox29.Text + "%'", conn)

        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid3.DataSource = dt
        conn.Close()
    End Sub

    Private Sub textBox9_TextChanged(sender As Object, e As EventArgs) Handles textBox9.TextChanged

    End Sub

    Private Sub textBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox10.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not textBox10.Text = "" Then
                textBox12.Focus()
            Else
                textBox10.Text = "0"
                textBox10.Enabled = False
                textBox12.Focus()
            End If

        End If
    End Sub

    Private Sub fillcombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Stores"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub

End Class