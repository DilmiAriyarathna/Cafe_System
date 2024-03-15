Imports System.Data.SqlClient

Public Class Form2
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Cafe_SYSDataSet.Usert' table. You can move, or remove it, as needed.
        ' Me.UsertTableAdapter.Fill(Me.Cafe_SYSDataSet.Usert)
        TextBox5.Select()
        ''''
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT count(Uid) FROM Usert"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox4.Text = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception

        End Try
        conn.Close()

        ''''
        FillCombo()
        Displaydata()
        Displaydata2()
        grd()
    End Sub
    Private Sub FillCombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select User_Div from User_Division"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''''''''''''''''
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 70
        BunifuCustomDataGrid1.Columns(1).Width = 220

        '''''
        BunifuCustomDataGrid2.Columns(0).Width = 50
        BunifuCustomDataGrid2.Columns(1).Width = 220
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim i = Convert.ToInt32(TextBox4.Text)
        i += 1
        TextBox4.Text = i.ToString()
        '''''
        If TextBox8.Text = "" Then
            MsgBox("Invalid Employee !")
            TextBox5.Focus()
            Exit Sub
        End If
        ''''
        If ComboBox2.Text = "" Then
            MsgBox("Invalid Project !")
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''''
        If ComboBox1.Text = "" Then
            MsgBox("Invalid User Division !")
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''''
        If TextBox1.Text = "" Then
            MsgBox("Invalid User Name !")
            TextBox1.Focus()
            Exit Sub
        End If
        ''''
        If TextBox2.Text = "" Then
            MsgBox("Invalid Password !")
            TextBox2.Focus()
            Exit Sub
        End If
        ''''
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Usert where UName = '" + TextBox1.Text + "' and pass = '" + TextBox2.Text + "' "
        cmd1 = New SqlCommand(SQL, conn)
        dt = New DataTable
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If x > 0 Then
            MsgBox("User Name and Password Already Exist !")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Focus()
            Exit Sub
        Else
            insert()
        End If
        conn.Close()


    End Sub
    Private Sub insert()
        If (TextBox2.Text = TextBox3.Text) Then
            conn = GetConnect()
            conn.Open()
            cmd = New SqlCommand("insert into Usert(Uid,UName,pass,div,emp_code,project,U_val,Acc_No) values ( " +
                " '" + TextBox4.Text + "', '" + TextBox1.Text + "','" + TextBox2.Text + "', " +
                " '" + TextBox9.Text + "' , '" + TextBox6.Text + "' , '" + ComboBox2.Text + "' , '" + TextBox10.Text + "', " +
                " '" + TextBox8.Text + "')", conn)
            cmd.ExecuteNonQuery()
            MsgBox("Update Successfully !")
            conn.Close()
            Clr()

            Displaydata()
        Else
            MsgBox("Please make sure your passwords match !")
            TextBox3.Text = ""
            TextBox3.Focus()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'Me.Hide()
        'Form3.Show()
    End Sub
    Public Sub Displaydata()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select empid as Code,Name from Employee", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Public Sub Displaydata2()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Uid as ID,UName as User_Name from Usert", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid2.DataSource = dt
        conn.Close()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clr()
    End Sub
    Private Sub Clr()
        Dim form2 As New Form2
        Me.Hide()
        form2.Show()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BunifuSeparator1_Load(sender As Object, e As EventArgs) Handles BunifuSeparator1.Load

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select empid as Code,Name from Employee where Name Like '%" + TextBox5.Text + "%'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox6.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(0).Value
            TextBox7.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            '  TextBox7.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
        End If
        ComboBox2.Focus()
        SendKeys.Send("{F4}")
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Acc_No from Employee where empid ='" + TextBox6.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox8.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select ID,val from User_Division where User_Div ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox9.Text = dr.GetValue(0).ToString
            TextBox10.Text = dr.GetValue(1).ToString
        End While
        conn.Close()
    End Sub
End Class