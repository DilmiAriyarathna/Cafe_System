Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form33
    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"
    ''
    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"
    ''
    Dim return_no, return_id As Integer
    Dim year, month, yr As String
    Dim invoice, order_num, credit, x, qty, qtytot As Integer

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button3.Focus()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        conn = GetConnect()
        conn.Open()
        SQL = "select GRN_No,Date from GRN_Det where Grn_inv = '" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox3.Text = dr.GetValue(0).ToString
            DateTimePicker2.Value = dr.GetValue(1).ToString
        End While
        conn.Close()
        ''
        grddata()
    End Sub
    Private Sub grddata()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select I_Code as Code,Discription,QTY,CAST(U_cost as numeric(17,2))Unit_Cost, " &
                                "CAST(total as numeric(17,2))Total " &
                                "from GRN_Details where Grn_inv='" + ComboBox1.Text + "'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid1.Columns(0).Width = 75
        BunifuCustomDataGrid1.Columns(1).Width = 250
        BunifuCustomDataGrid1.Columns(2).Width = 85
        BunifuCustomDataGrid1.Columns(3).Width = 100
        BunifuCustomDataGrid1.Columns(4).Width = 100
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox4.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            conn = GetConnect()
            conn.Open()
            SQL = "select Acc_No from Usert where UName ='" + ComboBox4.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                Label14.Text = dr.GetValue(0).ToString
            End While
            conn.Close()
            ''
            Button4.Focus()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Form35.TextBox1.Text = TextBox6.Text
        Form35.Show()
    End Sub

    Private Sub Clr()
        Dim Form33 As New Form33
        Me.Hide()
        Form33.Show()
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Clr()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox2.Text = "" Then
            MsgBox("Invalid Supplier !")
            Button2.Focus()
            Exit Sub
        End If
        ''
        If ComboBox5.Text = "" Then
            MsgBox("Invalid Store !")
            ComboBox5.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        If ComboBox3.Text = "" Then
            MsgBox("Invalid Reason !")
            ComboBox3.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        If ComboBox4.Text = "" Then
            MsgBox("Invalid User !")
            ComboBox4.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [item_Return] = [item_Return] + 1"
        cmd6 = New SqlCommand(SQL, conn)
        cmd6.ExecuteNonQuery()
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "truncate table Item_Return_tmp"
        cmd5 = New SqlCommand(SQL, conn)
        cmd5.ExecuteNonQuery()
        conn.Close()
        ''
        For r = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn.Open()
            SQL = "insert into Item_Return_tmp(Date,Time,Retutn_No,RTN,Supplier,Store,Invoice,Grn,Code,QTY,Unit_Cost, " &
                    "Total,Reason,note,Return_by,Flag)values( " &
                    "@Date,@Time,@Retutn_No,@RTN,@Supplier,@Store,@Invoice,@Grn,@Code,@QTY,@Unit_Cost,@Total, " &
                    "@Reason,@note,@Return_by,@Flag)"
            cmd = New SqlCommand(SQL, conn)
            With cmd.Parameters
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Time", t.ToString(formatb))
                .AddWithValue("@Retutn_No", TextBox6.Text)
                .AddWithValue("@RTN", Label3.Text)
                .AddWithValue("@Supplier", TextBox2.Text)
                .AddWithValue("@Store", Label12.Text)
                .AddWithValue("@Invoice", ComboBox1.Text)
                .AddWithValue("@Grn", TextBox3.Text)
                .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(r).Cells(0).Value)
                .AddWithValue("@QTY", BunifuCustomDataGrid1.Rows(r).Cells(2).Value)
                .AddWithValue("@Unit_Cost", BunifuCustomDataGrid1.Rows(r).Cells(3).Value)
                .AddWithValue("@Total", BunifuCustomDataGrid1.Rows(r).Cells(4).Value)
                .AddWithValue("@Reason", ComboBox3.Text)
                .AddWithValue("@note", TextBox4.Text)
                .AddWithValue("@Return_by", ComboBox4.Text)
                .AddWithValue("@Flag", "1")
            End With
            cmd.ExecuteNonQuery()
            conn.Close()
        Next
        ''
        For q = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn.Open()
            SQL = "insert into Stock(Code,Discription,Dr_qty,Unit_Cost,Total,Supplier,Store,Login, " &
                    "Date,Flag,Type,TXT,rtn)values( " &
                    "@Code,@Discription,@Dr_qty,@Unit_Cost,@Total,@Supplier,@Store,@Login, " &
                    "@Date,@Flag,@Type,@TXT,@rtn)"
            cmd1 = New SqlCommand(SQL, conn)
            With cmd1.Parameters
                .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(q).Cells(0).Value)
                .AddWithValue("@Discription", BunifuCustomDataGrid1.Rows(q).Cells(1).Value)
                .AddWithValue("@Dr_qty", BunifuCustomDataGrid1.Rows(q).Cells(2).Value)
                .AddWithValue("@Unit_Cost", BunifuCustomDataGrid1.Rows(q).Cells(3).Value)
                .AddWithValue("@Total", BunifuCustomDataGrid1.Rows(q).Cells(4).Value)
                .AddWithValue("@Supplier", TextBox2.Text)
                .AddWithValue("@Store", ComboBox5.Text)
                .AddWithValue("@Login", Label13.Text)
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Flag", "1")
                .AddWithValue("@Type", "Item_Return")
                .AddWithValue("@TXT", "Item_Return:" + ComboBox5.Text)
                .AddWithValue("@rtn", TextBox6.Text)
            End With
            cmd1.ExecuteNonQuery()
            conn.Close()
        Next
        ''
        For s = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn.Open()
            SQL = "insert into Stock(Code,Discription,Cr_qty,Unit_Cost,Total,Supplier,Store,Login, " &
                    "Date,Flag,Type,TXT)values( " &
                    "@Code,@Discription,@Cr_qty,@Unit_Cost,@Total,@Supplier,@Store,@Login, " &
                    "@Date,@Flag,@Type,@TXT)"
            cmd2 = New SqlCommand(SQL, conn)
            With cmd2.Parameters
                .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(s).Cells(0).Value)
                .AddWithValue("@Discription", BunifuCustomDataGrid1.Rows(s).Cells(1).Value)
                .AddWithValue("@Cr_qty", BunifuCustomDataGrid1.Rows(s).Cells(2).Value)
                .AddWithValue("@Unit_Cost", BunifuCustomDataGrid1.Rows(s).Cells(3).Value)
                .AddWithValue("@Total", BunifuCustomDataGrid1.Rows(s).Cells(4).Value)
                .AddWithValue("@Supplier", TextBox2.Text)
                .AddWithValue("@Store", ComboBox5.Text)
                .AddWithValue("@Login", Label13.Text)
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Flag", "1")
                .AddWithValue("@Type", "Item_Return")
                .AddWithValue("@TXT", "Item_Return:" + ComboBox5.Text)
                .AddWithValue("@rtn", TextBox6.Text)
            End With
            cmd2.ExecuteNonQuery()
            conn.Close()
        Next
        ''
        For j = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn.Open()
            SQL = "insert into Stock_Perform(Date,time,Code,Discription,Qty,Unit_Cost,Total, " &
            "Store,Login,Flag,Type,TXT, " &
            "acc_no,Invoice) values ( " &
            "@Date,@time,@Code,@Discription,@Qty,@Unit_Cost,@Total,@Store, " &
            "@Login,@Flag,@Type, " &
            "@TXT,@acc_no,@Invoice)"
            cmd4 = New SqlCommand(SQL, conn)
            With cmd4.Parameters
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Time", t.ToString(formatb))
                .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(j).Cells(0).Value)
                .AddWithValue("@Discription", BunifuCustomDataGrid1.Rows(j).Cells(1).Value)
                .AddWithValue("@QTY", BunifuCustomDataGrid1.Rows(j).Cells(2).Value)
                .AddWithValue("@Unit_cost", BunifuCustomDataGrid1.Rows(j).Cells(3).Value)
                .AddWithValue("@Total", BunifuCustomDataGrid1.Rows(j).Cells(4).Value)
                .AddWithValue("@Store", ComboBox5.Text)
                .AddWithValue("@Login", Label13.Text)
                .AddWithValue("@Flag", "1")
                .AddWithValue("@Type", "Item_Return")
                .AddWithValue("@TXT", "Item_Return:" + ComboBox5.Text)
                .AddWithValue("@acc_no", Label14.Text)
                .AddWithValue("@Invoice", TextBox6.Text)
            End With
            cmd4.ExecuteNonQuery()
            conn.Close()
        Next
        ''
        stkup()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update GRN_Details set flag = '5' "
        cmd6 = New SqlCommand(SQL, conn)
        cmd6.ExecuteNonQuery()
        conn.Close()
        ''
        conn.Open()
        SQL = "insert into Item_Return select * from Item_Return_tmp where Retutn_No = '" + TextBox6.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
        ''
        conn.Open()
        SQL = "update Item_Return_tmp set Time = '" + t.ToString(formatb) + "' " &
            " where Retutn_No = '" + TextBox6.Text + "' "
        cmd2 = New SqlCommand(SQL, conn)
        cmd2.ExecuteNonQuery()
        conn.Close()
        ''
        conn.Open()
        SQL = "update Item_Return set Time = '" + t.ToString(formatb) + "' " &
            " where  Retutn_No = '" + TextBox6.Text + "' "
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''
        MsgBox("Update Successfully !")
        ''
        Button6.Enabled = True
        Button6.Focus()
    End Sub
    Private Sub stkup()
        For w = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn = GetConnect()
            '  conn.Open()
            SQL = "select * from Stock_Qty where Code='" + BunifuCustomDataGrid1.Rows(w).Cells(0).Value + "'"
            cmd1 = New SqlCommand(SQL, conn)
            dt = New DataTable
            da = New SqlDataAdapter(cmd1)
            da.Fill(dt)
            x = Convert.ToInt32(dt.Rows.Count.ToString())
            If x > 0 Then
                conn.Open()
                SQL = "select * from Stock_Qty where Code='" + BunifuCustomDataGrid1.Rows(w).Cells(0).Value + "'"
                cmd = New SqlCommand(SQL, conn)
                dr = cmd.ExecuteReader
                While (dr.Read())
                    qty = dr.GetValue(1).ToString
                End While
                conn.Close()
                ''
                qtytot = qty - BunifuCustomDataGrid1.Rows(w).Cells(2).Value
                ''
                conn.Open()
                SQL = "Update Stock_Qty2 set QTY= @QTY,Final_Cost=@Final_Cost where Code='" + BunifuCustomDataGrid1.Rows(w).Cells(0).Value + "'"
                cmdq = New SqlCommand(SQL, conn)
                With cmdq.Parameters
                    .AddWithValue("@Code", BunifuCustomDataGrid1.Rows(w).Cells(0).Value)
                    .AddWithValue("@QTY", qtytot)
                    .AddWithValue("@Final_Cost", BunifuCustomDataGrid1.Rows(w).Cells(3).Value)
                End With
                cmdq.ExecuteNonQuery()
                conn.Close()
            End If
        Next
    End Sub
    Private Sub ComboBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            conn = GetConnect()
            conn.Open()
            SQL = "select Grn_inv from GRN_Det where Supp_Code='" + TextBox2.Text + "' and Store = '" + ComboBox5.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                ComboBox1.Items.Add(dr.GetString(0))
            End While
            conn.Close()
            ''
            conn.Open()
            SQL = "select Code from Stores where Name = '" + ComboBox5.Text + "'"
            cmd1 = New SqlCommand(SQL, conn)
            dr1 = cmd1.ExecuteReader
            While (dr1.Read())
                Label12.Text = dr1.GetValue(0).ToString
            End While
            conn.Close()
            ''
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form13.TextBox6.Text = "3"
        Form13.Show()
    End Sub

    Private Sub Form33_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(item_Return)item_Return from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            return_no = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''
        Label3.Text = return_no + 1
        ''
        year = Date.Now.Year.ToString()
        Month = Date.Now.Month.ToString()
        yr = year.Substring(year.Length - 2, 2)
        ''
        If month < 10 Then
            TextBox6.Text = "RTN:" + yr + "0" + month + ":" + Label3.Text
        Else
            TextBox6.Text = "RTN:" + yr + month + ":" + Label3.Text
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Stores"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox5.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select UName from Usert"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox4.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        If (Form1.setname = "") Then
            Label13.Text = ""
        Else
            Label13.Text = Form1.setname
        End If
    End Sub
End Class