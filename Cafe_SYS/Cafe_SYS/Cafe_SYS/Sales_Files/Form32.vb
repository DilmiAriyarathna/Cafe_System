Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form32
    Dim invoice, order_num, credit, x, qty, qtytot, yy As Integer
    Dim year, month, yr As String
    Dim table As New DataTable("Table")
    Dim table1 As New DataTable("Table1")
    Dim tot, dis, tot1, total, totdis, net, bal As Double
    ''
    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"
    ''
    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Panel4.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox19.Select()
        ''
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name,Contact1 as Contact,Address from Customers", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid2.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid2.Columns(0).Width = 75
        BunifuCustomDataGrid2.Columns(1).Width = 150
        BunifuCustomDataGrid2.Columns(2).Width = 85
        BunifuCustomDataGrid2.Columns(3).Width = 150
        ''
        Panel4.Visible = True
    End Sub

    Private Sub TextBox19_TextChanged(sender As Object, e As EventArgs) Handles TextBox19.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name,Contact1 as Contact,Address from Customers where Name like '%" + TextBox19.Text + "%'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid2.DataSource = dt
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid2.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox4.Text = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(0).Value
            TextBox1.Text = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(1).Value
            TextBox2.Text = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(2).Value
            TextBox3.Text = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(3).Value
        End If
        Panel4.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Form32_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''login user
        If (Form1.setname = "") Then
            Label3.Text = ""
        Else
            Label3.Text = Form1.setname
        End If
        ''
        Timer1.Start()
        ''
        BunifuCustomDataGrid1.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        BunifuCustomDataGrid1.DefaultCellStyle.Font = New Font("Verdena", 7)
        ''get usrnames 
        conn = GetConnect()
        conn.Open()
        SQL = "select UName from Usert"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''get inv number
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(invoice)invoice from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            invoice = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''inv increment
        Label13.Text = invoice + 1
        ''get order number
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(order_num)order_num from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            order_num = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''order num increment
        Label12.Text = order_num + 1
        ''create invoice Token number
        year = Date.Now.Year.ToString()
        month = Date.Now.Month.ToString()
        yr = year.Substring(year.Length - 2, 2)
        ''
        If month < 10 Then
            Label20.Text = yr + "/" + "0" + month + "/ INV : 0" + Label13.Text
        Else
            Label20.Text = yr + "/" + month + "/ INV : 0" + Label13.Text
        End If
        ''
        TextBox1.Text = "Cash Invoice"
        TextBox4.Text = "CH-001"
        ''
        BunifuCustomDataGrid2.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 6)
        ''
        BunifuCustomDataGrid2.DefaultCellStyle.Font = New Font("Verdena", 6)
        ''
        crgrid()
        ''
        grd()
        ''
        Panel5.Visible = True
        ''
        src()
        ''
        Me.KeyPreview = True
        ''
        Panel6.Left = 260
        Panel6.Top = 258
    End Sub
    Public Sub src()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select ID,Code,Name,CAST(R_Price as numeric(17,2))R_Price, " &
                                    "CAST(P_Price as numeric(17,2))P_Price,CAST(S_Price as numeric(17,2))S_Price from Products ", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid3.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid3.Columns(0).Width = 25
        BunifuCustomDataGrid3.Columns(1).Width = 100
        BunifuCustomDataGrid3.Columns(2).Width = 300
        BunifuCustomDataGrid3.Columns(3).Width = 100
        BunifuCustomDataGrid3.Columns(4).Width = 100
        BunifuCustomDataGrid3.Columns(5).Width = 100
    End Sub
    Private Sub TextBox11_Click(sender As Object, e As EventArgs) Handles TextBox11.Click
        If Panel5.Visible = False Then
            Panel5.Visible = True
        End If
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        If Label30.Text = "1" Then
            ''code
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select ID,Code,Name,CAST(R_Price as numeric(17,2))R_Price, " &
                                    "CAST(P_Price as numeric(17,2))P_Price,CAST(S_Price as numeric(17,2))S_Price from Products where Code Like '%" + TextBox11.Text + "%'", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid3.DataSource = dt
            conn.Close()
        ElseIf Label30.Text = "2" Then
            ''Discrip
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select ID,Code,Name,CAST(R_Price as numeric(17,2))R_Price, " &
                                    "CAST(P_Price as numeric(17,2))P_Price,CAST(S_Price as numeric(17,2))S_Price from Products where Name Like '%" + TextBox11.Text + "%'", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid3.DataSource = dt
            conn.Close()
        End If
    End Sub

    Private Sub Form32_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            If Label30.Text = "1" Then
                Label30.Text = "2"
            ElseIf Label30.Text = "2" Then
                Label30.Text = "1"
            End If
        End If
        ''
        If e.KeyCode = Keys.F2 Then
            textBox14.Focus()
        End If
        ''
        If e.KeyCode = Keys.F2 Then
            TextBox16.Focus()
        End If
    End Sub

    Private Sub BunifuCustomDataGrid3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid3.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = BunifuCustomDataGrid3.Rows(e.RowIndex)
            TextBox11.Text = row.Cells(1).Value.ToString
        End If
        Panel5.Visible = False
        textBox7.Select()
        filldata()
    End Sub
    Private Sub filldata()
        conn = GetConnect()
        conn.Open()
        SQL = "Select ID,Code,Name,CAST(R_Price As numeric(17,2))R_Price, " &
  "CAST(P_Price As numeric(17,2))P_Price,CAST(S_Price As numeric(17,2))S_Price from Products " &
        "where Code ='" + TextBox11.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        Using dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox23.Text = dr.GetValue(1).ToString
                TextBox5.Text = dr.GetValue(2).ToString
                TextBox20.Text = dr.GetValue(3).ToString
                TextBox21.Text = dr.GetValue(4).ToString
                TextBox22.Text = dr.GetValue(5).ToString
            End While
        End Using
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select CAST(N_sp as numeric(17,2))N_sp from Selling_Price where Code = '" + TextBox11.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        Using dr1 = cmd1.ExecuteReader
            While (dr1.Read())
                textBox6.Text = dr1.GetValue(0).ToString
                '   TextBox30.Text = dr1.GetValue(0).ToStrign
            End While
        End Using
        conn.Close()
    End Sub

    Private Sub TextBox23_TextChanged(sender As Object, e As EventArgs) Handles TextBox23.TextChanged
    End Sub

    Private Sub textBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            tot = Val(textBox6.Text) * Val(textBox7.Text)
            textBox8.Focus()
        End If
    End Sub

    Private Sub textBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox10.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            additem()
            ''
            Dim cntr As Integer

            cntr = BunifuCustomDataGrid1.Rows.Count
            TextBox24.Text = cntr
            ''
            total = total + Val(textBox10.Text)
            textBox13.Text = total.ToString("#.00")
            ''
            TextBox11.Text = ""
            TextBox23.Text = ""
            textBox5.Text = ""
            textBox6.Text = ""
            textBox7.Text = ""
            textBox8.Text = ""
            textBox9.Text = ""
            textBox10.Text = ""
            TextBox20.Text = ""
            TextBox21.Text = ""
            TextBox22.Text = ""
        End If
    End Sub

    Private Sub TextBox16_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub TextBox25_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox25.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            bal = Val(TextBox25.Text) - Val(textBox15.Text)
            TextBox12.Text = bal.ToString("#.00")

            Button4.Focus()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ComboBox1.Text = "" Then
            MsgBox("Invalid Cashier !")
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        If TextBox1.Text = "" And TextBox4.Text = "" Then
            MsgBox("Invalid Customer !")
            Button1_Click(sender, e)
            Exit Sub
        End If
        ''
        If textBox15.Text = "" Then
            MsgBox("Invalid Net Amount !")
            Exit Sub
        End If
        ''
        If TextBox25.Text = "" Then
            MsgBox("Invalid Received Amount !")
            TextBox25.Focus()
            Exit Sub
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "truncate table Invoice_Info_tmp"
        cmd5 = New SqlCommand(SQL, conn)
        cmd5.ExecuteNonQuery()
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [invoice] = [invoice] + 1 "
        cmd6 = New SqlCommand(SQL, conn)
        cmd6.ExecuteNonQuery()
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set  [order_num]=[order_num] + 1"
        cmdq = New SqlCommand(SQL, conn)
        cmdq.ExecuteNonQuery()
        conn.Close()
        ''
        For r = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn.Open()
            SQL = "insert into Invoice_Info_tmp(Code,Discription,Unit_cost,QTY,Tot,Dis,Dis_amt,Total, " &
                    "Date,Time,Login,Cashier,Acc_no,Order_No,Invoice_id,INV_Token,Inv_Type,Cus_Code, " &
                    "Discount,Net_Amt,Received,Balance,Pay_Type,Credit_card,Creditcrd_Type,bank, " &
                    "Pay_typeid,Pay_By,Flag,Flagq) values( " &
                    "@Code,@Discription,@Unit_cost,@QTY,@Tot,@Dis,@Dis_amt,@Total,@Date,@Time,@Login,@Cashier, " &
                    "@Acc_no,@Order_No,@Invoice_id,@INV_Token,@Inv_Type,@Cus_Code,@Discount,@Net_Amt,@Received, " &
                    "@Balance,@Pay_Type,@Credit_card,@Creditcrd_Type,@bank,@Pay_typeid,@Pay_By,@Flag,@Flagq)"
            cmd = New SqlCommand(SQL, conn)
            With cmd.Parameters
                .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(r).Cells(0).Value)
                .AddWithValue("@Discription", BunifuCustomDataGrid1.Rows(r).Cells(1).Value)
                .AddWithValue("@Unit_cost", BunifuCustomDataGrid1.Rows(r).Cells(3).Value)
                .AddWithValue("@QTY", BunifuCustomDataGrid1.Rows(r).Cells(2).Value)
                .AddWithValue("@Tot", BunifuCustomDataGrid1.Rows(r).Cells(4).Value)
                .AddWithValue("@Dis", BunifuCustomDataGrid1.Rows(r).Cells(5).Value)
                .AddWithValue("@Dis_amt", BunifuCustomDataGrid1.Rows(r).Cells(6).Value)
                .AddWithValue("@Total", BunifuCustomDataGrid1.Rows(r).Cells(7).Value)
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Time", t.ToString(formatb))
                .AddWithValue("@Login", Label3.Text)
                .AddWithValue("@Cashier", ComboBox1.Text)
                .AddWithValue("@Acc_no", Label45.Text)
                .AddWithValue("@Order_No", Label12.Text)
                .AddWithValue("@Invoice_id", Label13.Text)
                .AddWithValue("@INV_Token", Label20.Text)
                .AddWithValue("@Inv_Type", Label14.Text)
                .AddWithValue("@Cus_Code", TextBox4.Text)
                .AddWithValue("@Discount", textBox14.Text)
                .AddWithValue("@Net_Amt", textBox15.Text)
                .AddWithValue("@Received", TextBox25.Text)
                .AddWithValue("@Balance", TextBox12.Text)
                .AddWithValue("@Pay_Type", Label47.Text)
                .AddWithValue("@Credit_card", TextBox27.Text)
                .AddWithValue("@Creditcrd_Type", ComboBox3.Text)
                .AddWithValue("@bank", Label44.Text)
                .AddWithValue("@Pay_typeid", TextBox26.Text)
                .AddWithValue("@Pay_By", ComboBox1.Text)
                .AddWithValue("@Flag", "1")
                .AddWithValue("@Flagq", "I")
            End With
            cmd.ExecuteNonQuery()
            conn.Close()
        Next
        ''
        conn.Open()
        SQL = "insert into Invoice_Info select * from Invoice_Info_tmp where INV_Token = '" + Label20.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
        ''
        conn.Open()
        SQL = "update Invoice_Info set Time = '" + t.ToString(formatb) + "' " &
            " where  INV_Token = '" + Label20.Text + "' "
        cmd2 = New SqlCommand(SQL, conn)
        cmd2.ExecuteNonQuery()
        conn.Close()
        ''
        conn.Open()
        SQL = "update Invoice_Info_tmp set Time = '" + t.ToString(formatb) + "' " &
            " where INV_Token = '" + Label20.Text + "' "
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''
        prfm()
        ''
        For q = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn = GetConnect()
            '  conn.Open()
            SQL = "select * from Stock_Qty2 where Code='" + BunifuCustomDataGrid1.Rows(q).Cells(0).Value + "'"
            cmd1 = New SqlCommand(SQL, conn)
            dt = New DataTable
            da = New SqlDataAdapter(cmd1)
            da.Fill(dt)
            x = Convert.ToInt32(dt.Rows.Count.ToString())
            If x > 0 Then
                conn.Open()
                SQL = "select * from Stock_Qty2 where Code='" + BunifuCustomDataGrid1.Rows(q).Cells(0).Value + "'"
                cmd = New SqlCommand(SQL, conn)
                dr = cmd.ExecuteReader
                While (dr.Read())
                    qty = dr.GetValue(1).ToString
                End While
                conn.Close()
                ''
                qtytot = qty - BunifuCustomDataGrid1.Rows(q).Cells(2).Value
                ''
                conn.Open()
                SQL = "Update Stock_Qty2 set QTY= @QTY,Final_Cost=@Final_Cost where Code='" + BunifuCustomDataGrid1.Rows(q).Cells(0).Value + "'"
                cmdq = New SqlCommand(SQL, conn)
                With cmdq.Parameters
                    .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(q).Cells(0).Value)
                    .AddWithValue("@QTY", qtytot)
                    .AddWithValue("@Final_Cost", BunifuCustomDataGrid1.Rows(q).Cells(3).Value)
                End With
                cmdq.ExecuteNonQuery()
                conn.Close()
            End If
        Next
        ''
        MsgBox("Update Successfully !")
        ''
        Button6.Enabled = True
        Button6.Focus()
    End Sub
    Private Sub prfm()
        For j = 0 To BunifuCustomDataGrid2.RowCount - 1
            conn.Open()
            SQL = "insert into Stock_Perform(Date,time,Code,Discription,Qty,Unit_Cost,Total, " &
            "Store,Login,Cashier,Flag,Type,TXT, " &
            "acc_no,cus_code,Invoice,Order_num) values ( " &
            "@Date,@time,@Code,@Discription,@Qty,@Unit_Cost,@Total,@Store, " &
            "@Login,@Cashier,@Flag,@Type, " &
            "@TXT,@acc_no,@cus_code,@Invoice,@Order_num)"
            cmd4 = New SqlCommand(SQL, conn)
            With cmd4.Parameters
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Time", t.ToString(formatb))
                .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(j).Cells(0).Value)
                .AddWithValue("@Discription", BunifuCustomDataGrid1.Rows(j).Cells(1).Value)
                .AddWithValue("@QTY", BunifuCustomDataGrid1.Rows(j).Cells(2).Value)
                .AddWithValue("@Unit_cost", BunifuCustomDataGrid1.Rows(j).Cells(3).Value)
                .AddWithValue("@Total", BunifuCustomDataGrid1.Rows(j).Cells(7).Value)
                .AddWithValue("@Store", "A")
                .AddWithValue("@Login", Label3.Text)
                .AddWithValue("@Cashier", ComboBox1.Text)
                .AddWithValue("@Flag", "1")
                .AddWithValue("@Type", "Invoice")
                .AddWithValue("@TXT", "Invoice :" + Label20.Text)
                .AddWithValue("@acc_no", Label45.Text)
                .AddWithValue("@cus_code", TextBox4.Text)
                .AddWithValue("@Invoice", Label13.Text)
                .AddWithValue("@Order_num", Label12.Text)
            End With
            cmd4.ExecuteNonQuery()
            conn.Close()
        Next
    End Sub
    Private Sub TextBox25_Click(sender As Object, e As EventArgs) Handles TextBox25.Click
        If textBox14.Text = "" Then
            'textBox14.Text = "0"
            textBox14_KeyPress(sender, e)
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Bank"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox4.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(Credit)Credit from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            credit = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''
        TextBox26.Text = credit + 1
        ''
        TextBox28.Text = Label13.Text
        ''
        TextBox29.Text = textBox15.Text
        ''
        Panel6.Visible = True
        ''
        TextBox27.Focus()
        ''
        BunifuCustomDataGrid4.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 6)
        ''
        BunifuCustomDataGrid4.DefaultCellStyle.Font = New Font("Verdena", 6)
        ''
        table1.Columns.Add("ID", Type.GetType("System.Int32"))
        table1.Columns.Add("Inv_No", Type.GetType("System.String"))
        table1.Columns.Add("Total_Amt", Type.GetType("System.Double"))
        table1.Columns.Add("Card_No", Type.GetType("System.String"))
        table1.Columns.Add("Card_Type", Type.GetType("System.String"))
        table1.Columns.Add("Bank", Type.GetType("System.String"))

        BunifuCustomDataGrid4.DataSource = table1
        ''
        BunifuCustomDataGrid4.Columns(0).Width = 50
        BunifuCustomDataGrid4.Columns(1).Width = 85
        BunifuCustomDataGrid4.Columns(2).Width = 85
        BunifuCustomDataGrid4.Columns(3).Width = 85
        BunifuCustomDataGrid4.Columns(4).Width = 85
        BunifuCustomDataGrid4.Columns(5).Width = 100

    End Sub

    Private Sub ComboBox4_TextChanged(sender As Object, e As EventArgs) Handles ComboBox4.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Bank where Name ='" + ComboBox4.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label44.Text = dr.GetValue(1).ToString
        End While
        conn.Close()
    End Sub

    Private Sub TextBox27_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox27.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox3.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox4.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button14.Focus()
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Panel6.Visible = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form34.Show()
    End Sub

    Private Sub BunifuCustomDataGrid2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid2.CellContentClick

    End Sub

    Private Sub TextBox19_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox19.KeyPress
        conn = GetConnect()
        conn.Open()
        SQL = "select Code,Name,Contact1 as Contact,Address from Customers where Name like '%" + TextBox19.Text + "%'"
        cmd = New SqlCommand(SQL, conn)
        dt = New DataTable()
        da = New SqlDataAdapter(cmd)
        da.Fill(dt)
        yy = Convert.ToInt32(dt.Rows.Count.ToString())
        If (yy = 0) Then
            Dim result As DialogResult = MsgBox("There is no customer by this name. Do you want to create this customer? ?", vbYesNo, "The Garden Cafe")
            If (result = vbYes) Then
                Form29.Show()
                Form29.TextBox2.Text = TextBox19.Text
                Form29.Label9.Text = "55"
            Else
                Panel4.Visible = False
            End If

        End If
        conn.Close()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If TextBox29.Text = "" Then
            MsgBox("Invalid Bill Amount !")
            TextBox29.Focus()
            Exit Sub
        End If
        ''
        If Label44.Text = "" Then
            MsgBox("Invalid Bank !")
            ComboBox4.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        If ComboBox3.Text = "" Then
            MsgBox("Invalid Card Type !")
            ComboBox3.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        If TextBox27.Text = "" Then
            MsgBox("Invalid Card Number !")
            TextBox27.Focus()
            Exit Sub
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "insert into INV_Credit_Payment(ID,Invoice,Amount,CC_num,CC_type,Bank,flag,Cashier,Date,Time) " &
                " Values ('" + TextBox26.Text + "','" + TextBox28.Text + "','" + TextBox29.Text + "' ," &
                " '" + TextBox27.Text + "','" + ComboBox3.Text + "','" + Label44.Text + "','1', " &
                " '" + Label45.Text + "', '" + d.ToString(formata) + "' , '" + t.ToString(formatb) + "')"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''
        Label47.Text = "Credit Card"
        ''
        MsgBox("Done !")
        ''
        table1.Rows.Add(TextBox26.Text, TextBox28.Text, TextBox29.Text, TextBox27.Text, ComboBox3.Text,
                        ComboBox4.Text)
        BunifuCustomDataGrid4.DataSource = table1
        ''
        TextBox25.Text = textBox15.Text
        ''
        TextBox25_KeyPress(sender, e)
    End Sub
    Private Sub Clr()
        Dim Form32 As New Form32
        Me.Hide()
        Form32.Show()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Clr()
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Acc_No from Usert where UName ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label45.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid3_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles BunifuCustomDataGrid3.CellMouseClick
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Form_S.TextBox5.Text = "5"
        Form_S.Show()
    End Sub

    Private Sub textBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox14.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not textBox14.Text = "" Then
                totdis = Val(Val(textBox13.Text) * Val(textBox14.Text)) / 100
                net = total - totdis
                textBox15.Text = net.ToString("#.00")
            ElseIf textBox14.Text = "" Then
                totdis = 0
                textBox14.Text = 0
                net = total - totdis
                textBox15.Text = net.ToString("#.00")
            End If
            TextBox25.Focus()
            Button10.Enabled = True
        End If
    End Sub

    Private Sub textBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox8.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not textBox8.Text = "" Then
                dis = Val(tot * textBox8.Text) / 100
                textBox9.ReadOnly = True
                textBox9.Focus()
            ElseIf textBox8.Text = "" Then
                dis = 0
                textBox9.Focus()
            End If
        End If
    End Sub

    Private Sub textBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox9.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            tot1 = tot - dis - Val(Val(textBox9.Text) * Val(textBox8.Text))
            textBox10.Text = tot1.ToString("#.00")
            textBox10.Focus()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label4.Text = DateTime.Now.ToString("hh:mm:ss tt")
        Label6.Text = DateTime.Now.ToString("MM/dd/yyyy")
    End Sub
    Private Sub additem()
        table.Rows.Add(TextBox23.Text, textBox5.Text, textBox7.Text, textBox6.Text, tot, textBox8.Text, textBox9.Text,
                       textBox10.Text, TextBox20.Text, TextBox22.Text, TextBox21.Text)
        BunifuCustomDataGrid1.DataSource = table
    End Sub
    Private Sub crgrid()
        table.Columns.Add("Code", Type.GetType("System.String"))
        table.Columns.Add("Discription", Type.GetType("System.String"))
        table.Columns.Add("QTY", Type.GetType("System.Int32"))
        table.Columns.Add("Unit_Cost", Type.GetType("System.String"))
        table.Columns.Add("Total", Type.GetType("System.Double"))
        table.Columns.Add("Discount", Type.GetType("System.String"))
        table.Columns.Add("Discount_Amt", Type.GetType("System.String"))
        table.Columns.Add("Total_Amt", Type.GetType("System.String"))
        table.Columns.Add("R_Price", Type.GetType("System.String"))
        table.Columns.Add("S_Price", Type.GetType("System.String"))
        table.Columns.Add("P_Price", Type.GetType("System.String"))

        BunifuCustomDataGrid1.DataSource = table
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 100
        BunifuCustomDataGrid1.Columns(1).Width = 250
        BunifuCustomDataGrid1.Columns(2).Width = 85
        BunifuCustomDataGrid1.Columns(3).Width = 85
        BunifuCustomDataGrid1.Columns(4).Width = 100
        BunifuCustomDataGrid1.Columns(5).Width = 100
        BunifuCustomDataGrid1.Columns(6).Width = 100
        BunifuCustomDataGrid1.Columns(7).Width = 100
        BunifuCustomDataGrid1.Columns(8).Width = 100
        BunifuCustomDataGrid1.Columns(9).Width = 100
        BunifuCustomDataGrid1.Columns(10).Width = 100
    End Sub
End Class