Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form11
    'Public conn As New SqlConnection
    'Dim SQL As String
    'Dim cmd, cmd1 As SqlCommand
    'Dim da As New SqlDataAdapter
    'Dim ds As New DataSet
    'Dim dr As SqlDataReader
    'Dim dt As DataTable
    'Dim dv As DataView
    Dim tmpcnt As Integer
    Dim user, mcat, scat, dt1 As String
    Dim gtot As Double
    Dim totl As Double
    Dim table As New DataTable("Table")
    ''
    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"

    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"
    ''''''''''''''''''''''''''''''''''''''''

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button12.Enabled = False
        Button11.Enabled = False
        '  Button9_Click(Nothing, Nothing)
        fnldate()
        count()

        ''''''''''''''''''''''''''''''''''''
        dt1 = Date.Now.Year.ToString()  ''''get only year
        Label14.Text = dt1.Substring(dt1.Length - 2, 2)   'get last two digits
        '''''''''''''''''''''''''''''''''''''''
        FillCombo()
        ''''''''''''
        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If
        ''
        crgrid()
        ''
        BunifuCustomDataGrid1.Columns(1).Width = 250
        ''
        BunifuCustomDataGrid1.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        BunifuCustomDataGrid1.DefaultCellStyle.Font = New Font("Verdena", 7)
    End Sub
    Private Sub crgrid()
        ' table.Columns.Add("Id", Type.GetType("System.Int32"))
        table.Columns.Add("Code", Type.GetType("System.String")) ' 0
        table.Columns.Add("Discription", Type.GetType("System.String")) '1
        table.Columns.Add("QTY", Type.GetType("System.Int32")) '2
        table.Columns.Add("Unit_Cost", Type.GetType("System.String")) '3
        table.Columns.Add("Total", Type.GetType("System.Double")) '4
        table.Columns.Add("PO_No", Type.GetType("System.String")) '5
        table.Columns.Add("Store", Type.GetType("System.String")) '6
        table.Columns.Add("Div", Type.GetType("System.String")) '7
        table.Columns.Add("F_Date", Type.GetType("System.String")) '8
        table.Columns.Add("User", Type.GetType("System.String")) '9
        table.Columns.Add("Supplier", Type.GetType("System.String")) ' 10
        table.Columns.Add("Supplier_Code", Type.GetType("System.String")) '11

        BunifuCustomDataGrid1.DataSource = table
    End Sub

    Private Sub Additem()
        table.Rows.Add(TextBox5.Text, TextBox4.Text, TextBox7.Text, TextBox8.Text, totl, TextBox3.Text, ComboBox1.Text,
                      ComboBox2.Text, DateTimePicker1.Value, user, TextBox1.Text, TextBox2.Text)
        BunifuCustomDataGrid1.DataSource = table
    End Sub
    Private Sub fnldate()
        DateTimePicker1.Value = DateTimePicker1.Value.AddDays(10)
    End Sub
    Private Sub count()
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(PO)PO from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label16.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''
        Dim cc = Convert.ToInt32(Label16.Text)
        cc += 1
        Label17.Text = cc.ToString()
        ''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Count(Id) from Purchase_Order_tmp"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                tmpcnt = dr.GetValue(0).ToString
            End While
        Catch ex As Exception
        End Try
        conn.Close()
    End Sub
    Private Sub FillCombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Stores"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select User_Div from User_Division"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox1.Checked = True Then
                TextBox3.Focus()
            Else
                'Dim cc = Convert.ToInt32(TextBox6.Text)
                'cc += 1
                'Label9.Text = cc.ToString()

                TextBox3.Text = Label10.Text + "/" + Label14.Text + "/" + "PO" + "/" + "00" + Label17.Text
            End If
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
        End If


    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub Label18_TextChanged(sender As Object, e As EventArgs) Handles Label18.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select CAST(P_Price as numeric(17,2))P_Price from Item " &
                " where Code = '" + Label18.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox8.Text = dr.GetValue(0).ToString
            'TextBox4.Text = dr.GetValue(1).ToString   'con1
            'TextBox5.Text = dr.GetValue(2).ToString    'con2
            'TextBox3.Text = dr.GetValue(3).ToString    'add
            'TextBox7.Text = dr.GetValue(4).ToString    'emil
            'TextBox8.Text = dr.GetValue(5).ToString    'reg
            'TextBox9.Text = dr.GetValue(6).ToString   'acc
            'ComboBox1.Text = dr.GetValue(7).ToString   'bnk
        End While

        conn.Close()
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then

            Dim qty As Double

            Dim u_cost As Double


            qty = Val(TextBox7.Text)
            u_cost = Val(TextBox8.Text)
            totl = (Val(TextBox7.Text) * u_cost)
            TextBox9.Text = Val(totl.ToString("#.00"))

            'TextBox9.Text = Val(TextBox7.Text) * Val(TextBox8.Text)

            gtot = gtot + totl
            TextBox10.Text = gtot.ToString("#.00")

            Additem()

            TextBox7.Text = ""
            Button4_Click(sender, e)
        End If
    End Sub
    Private Sub ADD()
        For i = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Purchase_Order2(Id,Date,Time,Login,Div,Store,Supplier,PO_num, " &
            "F_Date,Item,Discrip,QTY,u_cost,total,Flag,I_flag,gtot)values( " &
            "@Id,@Date,@Time,@Login,@Div,@Store,@Supplier,@PO_num,@F_Date, " &
            "@Item,@Discrip,@QTY,@u_cost,@total,@Flag,@I_flag,@gtot)"
            cmd = New SqlCommand(SQL, conn)
            With cmd
                .Parameters.AddWithValue("@Id", Label17.Text)
                .Parameters.AddWithValue("@Date", d.ToString(formata))
                .Parameters.AddWithValue("@Time", t.ToString(formatb))
                .Parameters.AddWithValue("@Login", user)

                .Parameters.AddWithValue("@Div", BunifuCustomDataGrid1.Rows(i).Cells(7).Value)
                .Parameters.AddWithValue("@Store", BunifuCustomDataGrid1.Rows(i).Cells(6).Value)
                .Parameters.AddWithValue("@Supplier", BunifuCustomDataGrid1.Rows(i).Cells(11).Value)
                .Parameters.AddWithValue("@PO_num", BunifuCustomDataGrid1.Rows(i).Cells(5).Value)
                .Parameters.AddWithValue("@F_Date", BunifuCustomDataGrid1.Rows(i).Cells(8).Value)
                .Parameters.AddWithValue("@Item", BunifuCustomDataGrid1.Rows(i).Cells(0).Value)
                .Parameters.AddWithValue("@Discrip", BunifuCustomDataGrid1.Rows(i).Cells(1).Value)
                .Parameters.AddWithValue("@QTY", BunifuCustomDataGrid1.Rows(i).Cells(2).Value)
                .Parameters.AddWithValue("@u_cost", BunifuCustomDataGrid1.Rows(i).Cells(3).Value)
                .Parameters.AddWithValue("@total", BunifuCustomDataGrid1.Rows(i).Cells(4).Value)

                .Parameters.AddWithValue("@Flag", "1")
                .Parameters.AddWithValue("@I_flag", "1")
                .Parameters.AddWithValue("@gtot", TextBox10.Text)
                .ExecuteNonQuery()
            End With
            conn.Close()
        Next
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Form13.refreshme()
        clr()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        conn = GetConnect()
        conn.Open()
        SQL = "truncate table Purchase_Order2"
        cmd5 = New SqlCommand(SQL, conn)
        cmd5.ExecuteNonQuery()
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [PO] = [PO] + 1"
        cmd6 = New SqlCommand(SQL, conn)
        cmd6.ExecuteNonQuery()
        conn.Close()
        ''
        ADD()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "insert into Purchase_Order select * from Purchase_Order2 where PO_num = '" + TextBox3.Text + "'"
        cmd4 = New SqlCommand(SQL, conn)
        cmd4.ExecuteNonQuery()
        conn.Close()

        ''''''
        conn = GetConnect()
        conn.Open()
        SQL = "update Purchase_Order set Time ='" + t.ToString(formatb) + "'  where PO_num = '" + TextBox3.Text + "'"
        cmd2 = New SqlCommand(SQL, conn)
        cmd2.ExecuteNonQuery()
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update Purchase_Order set Time ='" + t.ToString(formatb) + "'  where PO_num = '" + TextBox3.Text + "'"
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        '''''''''''''

        ''''''''''''''''''''''''''
        MsgBox("Update Successfully !")
        'clr()
        Button10.Enabled = False
        Button11.Enabled = True

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Form16.TextBox6.Text = "1"
        Form16.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Form17.Show()


    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Panel3.Visible = True
        TextBox15.Focus()
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged

        '''''''''''
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Id,Item As Code,Discrip as Discription,QTY,CAST(u_cost as numeric(17,2))Unit_Cost, " &
                                "CAST(total as numeric(17,2))Total,Supplier from Purchase_Order where PO_num = '" + TextBox13.Text + "'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = " select PO_num,Div,Store,Supplier,scode,F_Date,CAST(Total as numeric(17,2))Total from PO_vw where PO_num = '" + TextBox13.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            TextBox3.Text = dr.GetValue(0).ToString
            ComboBox2.Text = dr.GetValue(1).ToString
            ComboBox1.Text = dr.GetValue(2).ToString
            TextBox1.Text = dr.GetValue(3).ToString
            TextBox2.Text = dr.GetValue(4).ToString
            DateTimePicker1.Value = dr.GetValue(5).ToString
            TextBox10.Text = dr.GetValue(6).ToString
        End If
        conn.Close()

    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged

    End Sub
    Private Sub deletepo()
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"
        '''''
        conn = GetConnect()
        conn.Open()
        SQL = "update Purchase_Order set Flag = '2' , I_flag = '2',Date='" + d.ToString(formata) + "', " &
                " Time ='" + t.ToString(formatb) + "' where PO_num = '" + TextBox13.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub TextBox15_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox15.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If TextBox15.Text = "11918" Then
                Dim result As DialogResult = MsgBox("Do you want Delete PO ?", vbYesNo, "The Garden Cafe")
                If (result = vbYes) Then
                    deletepo()
                    MsgBox("Successfully Deleted !")
                End If
            ElseIf Not TextBox15.Text = "11918" Then
                MsgBox("Invalid User Authentication !")
                TextBox15.Text = ""
                TextBox15.Focus()
            End If
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged

    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged

    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button4_Click(sender, e)
        End If
    End Sub

    Private Sub display1()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Id,Item,Discrip as Discription,QTY,CAST(u_cost as numeric(17,2))Unit_Cost, " &
                                "CAST(total as numeric(17,2))Total,Supplier from Purchase_Order_tmp", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub clr()
        Dim form11 As New Form11
        Me.Hide()
        form11.Show()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form5.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form12.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form9.TextBox6.Text = "2"
        Form9.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form13.TextBox6.Text = "2"
        Form13.Show()
    End Sub
End Class