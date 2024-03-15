Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form31
    Dim x As Integer
    Dim user As String

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox1.BackColor = Color.Tan
            CheckBox2.Enabled = False
        Else
            CheckBox2.Enabled = True
            CheckBox1.BackColor = Color.LightGray
        End If
    End Sub
    Private Sub clr()
        Dim Form31 As New Form31
        Me.Hide()
        Form31.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox2.BackColor = Color.Tan
        Else
            CheckBox1.Enabled = True
            CheckBox2.BackColor = Color.LightGray
        End If
    End Sub
    Private Sub Display()
        conn = GetConnect()
        conn.Open()
        SQL = "select ID,Name as Customer_Name,Code as Customer_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
                "Address,Email from Customers"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 50  'id
        BunifuCustomDataGrid1.Columns(1).Width = 250    'name
        BunifuCustomDataGrid1.Columns(2).Width = 120    'code
        BunifuCustomDataGrid1.Columns(3).Width = 100    'cnt1
        BunifuCustomDataGrid1.Columns(4).Width = 100    'cnt2
        BunifuCustomDataGrid1.Columns(5).Width = 250    'add
        BunifuCustomDataGrid1.Columns(6).Width = 150    'email
        ''BunifuCustomDataGrid1.Columns(7).Width = 100    'rgst
        ''BunifuCustomDataGrid1.Columns(8).Width = 150    'bank
        ''BunifuCustomDataGrid1.Columns(9).Width = 100    'acc
    End Sub

    Private Sub Form31_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
        grd()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clr()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If CheckBox1.Checked = True Then
            scr1()
        ElseIf CheckBox2.Checked = True Then
            scr2()
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("Invalid Option !")
            TextBox1.Text = ""
            TextBox1.Focus()
            Exit Sub
        End If
    End Sub
    Private Sub scr1()
        conn = GetConnect()
        conn.Open()
        'SQL = "select ID,Name as Supplier_Name,Code as Sup_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
        '        "Address,Email,regstr_no as Registerd_No,Bank ,accno as Acc_No from Supplier where " &
        '        " Name like '%" + TextBox1.Text + "%'"
        SQL = "select ID,Name as Customer_Name,Code as Customer_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
                "Address,Email from Customers where  Name like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub scr2()
        conn = GetConnect()
        conn.Open()
        'SQL = "select ID,Name as Supplier_Name,Code as Sup_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
        '        "Address,Email,regstr_no as Registerd_No,Bank ,accno as Acc_No from Supplier where " &
        '        "  Code like '%" + TextBox1.Text + "%'"
        SQL = "select ID,Name as Customer_Name,Code as Customer_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
                "Address,Email from Customers where  Code like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox5.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            TextBox4.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
            TextBox7.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
        End If
        Me.Hide()
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If TextBox6.Text = "1" Then
            Form30.TextBox1.Text = TextBox5.Text
            Form30.TextBox2.Text = TextBox4.Text
            Form30.Label18.Text = TextBox4.Text
            Form30.TextBox4.Focus()
        End If
    End Sub
End Class