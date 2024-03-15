Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form3
    'Public conn As New SqlConnection
    'Dim SQL As String
    'Dim cmd, cmd1 As SqlCommand
    'Dim da As New SqlDataAdapter
    'Dim ds As New DataSet
    'Dim dr As SqlDataReader
    'Dim dt As DataTable
    'Dim dv As DataView
    Dim x, tmpcnt, cnt As Integer
    Dim user, mcat, scat, year, month As String
    Dim gtot, nettot As Double
    Dim total, dis1, dis2 As Double
    Dim table As New DataTable("Table")

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"

    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"

    Private Sub Form3_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            If Label18.Text = "Description" Then
                Label18.Text = "Code"
                TextBox5.Focus()
            ElseIf Label18.Text = "Code" Then
                Label18.Text = "Description"
                TextBox6.Focus()
            End If
        End If
        ''''
        If e.KeyCode = Keys.Escape Then
            ''If Panel3.Visible = True Then
            ''    Panel3.Visible = False
            ''ElseIf Panel3.Visible = False Then
            ''    Panel3.Visible = True
            ''End If
            Form42.Label3.Text = "1"
            Form42.Show()
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name from Products where Code like '%" + TextBox5.Text + "%'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid2.DataSource = dt
        conn.Close()
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name from Products where Name like '%" + TextBox6.Text + "%'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid2.DataSource = dt
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid2.CellClick
        'While (!parser.EndOfData)
        While (e.RowIndex) > 0
            If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
                TextBox5.Text = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(0).Value
                TextBox12.Text = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(0).Value
                TextBox6.Text = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(1).Value
                TextBox8.Focus()
                Panel3.Visible = False
            End If
            '   parser.Close()
        End While
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "Select CAST(N_sp As numeric(17,2))N_sp  from Selling_Price where Code ='" + TextBox12.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox7.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub
    Private Sub tot()
        total = Val(TextBox7.Text) * Val(TextBox8.Text)
        ''
        If Not TextBox9.Text = "" Then
            dis1 = Val(total) * Val(Val(TextBox9.Text) / 100)
        ElseIf TextBox9.Text = "" Then
            dis1 = 0
        End If
        ''
        If Val(dis1) = 0 Then
            dis2 = Val(TextBox10.Text) * Val(TextBox8.Text)
        ElseIf Not Val(dis1) = 0 Then
            dis2 = 0
        End If
        ''
        gtot = total - dis1 - dis2
        TextBox11.Text = gtot.ToString("#.00")
        ''
        nettot = nettot + gtot
        TextBox13.Text = nettot.ToString("#.00")
    End Sub
    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox9.Focus()
        End If
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            '' If Not TextBox9.Text = "" And TextBox10.Text = "" Then
            tot()
            Additem()
            ''
            conn = GetConnect()
                conn.Open()
            SQL = "insert into Order_Details(Date,Time,ID,Order_No,Code,Discription,Unit_Cost,QTY,Dis,Dis_amt,Total, " &
                   "Login,Cashier,customer,cus_code,flag,flagn) values('" + d.ToString(formata) + "' ,  " &
                   " '" + t.ToString(formatb) + "' , '" + TextBox3.Text + "' , '" + TextBox4.Text + "' , " &
                   " '" + TextBox5.Text + "' , '" + TextBox6.Text + "' , '" + TextBox7.Text + "' , " &
                   " '" + TextBox8.Text + "' , '" + TextBox9.Text + "' ,'" + TextBox10.Text + "' , '" + TextBox11.Text + "' , " &
                   " '" + user + "' , '" + ComboBox1.Text + "' , '" + TextBox14.Text + "' , '" + TextBox15.Text + "' , " &
                   " '1','1')"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            conn.Close()

            ''
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox12.Text = ""
        End If
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox10.Focus()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        clr
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox1.Checked = True Then
                TextBox4.Focus()
            Else
                ''create order number
                Dim i = Convert.ToInt32(Label23.Text)
                i += 1
                Label24.Text = i.ToString()

                TextBox4.Text = Label22.Text + "/" + month + "/" + Label24.Text
                ''
                TextBox5.Focus()
            End If
            Form42.Label3.Text = "1"
            Form42.Show()
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        conn = GetConnect()
        conn.Open()
        SQL = "truncate table Order_Detail_t"
        cmd5 = New SqlCommand(SQL, conn)
        cmd5.ExecuteNonQuery()
        conn.Close()
        ''''
        ' conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [order_num] = [order_num] + 1"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''''
        conn.Open()
        SQL = "insert into Order_Details2 select * from Order_Details where Order_No = '" + TextBox4.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
        ''''
        conn.Open()
        SQL = "insert into Order_Detail_t select * from Order_Details where Order_No = '" + TextBox4.Text + "'"
        cmd4 = New SqlCommand(SQL, conn)
        cmd4.ExecuteNonQuery()
        conn.Close()
        ''''
        conn.Open()
        SQL = "update Order_Details set Time = '" + t.ToString(formatb) + "' , G_tot = '" + TextBox13.Text + "' " &
            " where  Order_No = '" + TextBox4.Text + "' "
        cmd2 = New SqlCommand(SQL, conn)
        cmd2.ExecuteNonQuery()
        conn.Close()
        ''''
        conn.Open()
        SQL = "update Order_Details2 set Time = '" + t.ToString(formatb) + "' , G_tot = '" + TextBox13.Text + "' " &
            " where  Order_No = '" + TextBox4.Text + "' "
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''''
        conn.Open()
        SQL = "update Order_Detail_t set Time = '" + t.ToString(formatb) + "' , G_tot = '" + TextBox13.Text + "' " &
            " where  Order_No = '" + TextBox4.Text + "' "
        cmd6 = New SqlCommand(SQL, conn)
        cmd6.ExecuteNonQuery()
        conn.Close()

        MsgBox("Update Successfully !")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form21.TextBox6.Text = "1"
        Form21.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Form22.Show()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            CheckBox1.Focus()
            CheckBox1.BackColor = Color.Tan
        End If
    End Sub
    Private Sub clr()
        Dim form3 As New Form3
        Me.Hide()
        form3.Show()
    End Sub
    Private Sub fillcombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select UName from Usert"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = DateTime.Now.ToString()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If
        ''''
        If Not user = "" Then
            Label4.Text = user
        End If
        ''''
        Timer1.Start()
        ''''
        fillcombo()
        ComboBox1.Select()
        ComboBox1.DroppedDown = True
        ''''
        '  Panel3.Visible = True
        ''''
        Me.KeyPreview = True
        ''''
        year = Date.Now.Year.ToString()
        month = Date.Now.Month.ToString()
        Label22.Text = year.Substring(year.Length - 2, 2)
        ''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(order_num)order_num from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label23.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''''
        Dim i = Convert.ToInt32(Label23.Text)
        i += 1
        TextBox3.Text = i.ToString()
        ''''
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name from Products ", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid2.DataSource = dt
        conn.Close()
        ''''
        grd()
        ''
        crgrid()
        BunifuCustomDataGrid3.Columns(1).Width = 300
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid2.Columns(0).Width = 85
        BunifuCustomDataGrid2.Columns(1).Width = 275
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub crgrid()
        table.Columns.Add("Code", Type.GetType("System.String"))
        table.Columns.Add("Discription", Type.GetType("System.String"))
        table.Columns.Add("Unit_Cost", Type.GetType("System.String"))
        table.Columns.Add("QTY", Type.GetType("System.Int32"))
        table.Columns.Add("Discount", Type.GetType("System.String"))
        table.Columns.Add("Discount_Amt", Type.GetType("System.String"))
        table.Columns.Add("Total", Type.GetType("System.String"))
        BunifuCustomDataGrid3.DataSource = table
    End Sub
    Private Sub Additem()
        table.Rows.Add(TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox8.Text,
                      TextBox9.Text, TextBox10.Text, TextBox11.Text)
        BunifuCustomDataGrid3.DataSource = table

        '''''
        'Dim d As DateTime = DateTime.Now
        'Dim formata As String = "MM/dd/yyyy"

        'Dim t As DateTime = DateTime.Now
        'Dim formatb As String = "hh:mm:ss tt"
        ''''''
        'For Each row As DataGridViewRow In BunifuCustomDataGrid1.Rows
        '    conn = GetConnect()
        '    conn.Open()
        '    SQL = "insert into Order_Details(Date,Time,ID,Order_No,Code,Discription,Unit_Cost,QTY,Dis,Dis_amt,Total, " &
        '           "Login,Cashier,customer,cus_code,flag,flagn) values(@Date,@Time,@ID,@Order_No,@Code,@Discription, " &
        '           "@Unit_Cost,@QTY,@Dis,@Dis_amt,@Total,@Login,@Cashier,@customer,@cus_code,@flag,@flagn)"
        '    cmd = New SqlCommand(SQL, conn)
        '    cmd.Parameters.AddWithValue("@Date", d.ToString(formata))
        '    cmd.Parameters.AddWithValue("@Time", t.ToString(formatb))
        '    cmd.Parameters.AddWithValue("@ID", TextBox3.Text)
        '    cmd.Parameters.AddWithValue("@Order_No", TextBox4.Text)
        '    cmd.Parameters.AddWithValue("@Code", row.Cells("Code").Value)
        '    cmd.Parameters.AddWithValue("@Discription", row.Cells("Discription").Value)
        '    cmd.Parameters.AddWithValue("@Unit_Cost", row.Cells("Unit_Cost").Value)
        '    cmd.Parameters.AddWithValue("@QTY", row.Cells("QTY").Value)
        '    cmd.Parameters.AddWithValue("@Dis", row.Cells("Discount").Value)
        '    cmd.Parameters.AddWithValue("@Dis_amt", row.Cells("Discount_Amt").Value)
        '    cmd.Parameters.AddWithValue("@Total", row.Cells("Total").Value)
        '    cmd.Parameters.AddWithValue("@Login", user)
        '    cmd.Parameters.AddWithValue("@Cashier", ComboBox1.Text)
        '    cmd.Parameters.AddWithValue("@customer", TextBox14.Text)
        '    cmd.Parameters.AddWithValue("@cus_code", TextBox15.Text)
        '    cmd.Parameters.AddWithValue("@flag", "1")
        '    cmd.Parameters.AddWithValue("@flagn", "2")
        '    cmd.ExecuteNonQuery()
        '    conn.Close()
        'Next
        'MsgBox("Done !")
    End Sub

    Private Sub Form3_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged

    End Sub
End Class