Imports System.Data.SqlClient
Public Class Form1
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As New SqlDataAdapter
    Dim ds, ds1 As New DataSet
    Dim dr, dr1 As SqlDataReader
    Dim dt, dt1 As DataTable
    Dim dv As DataView
    Dim x As Integer
    Dim empnm As String
    Dim div As String
    Public setname As String
    Public setempname As String
    Public setdiv As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MsgBox("Do you want exit ?", vbYesNo, "The Garden Cafe")
        If (result = vbYes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'conn = GetConnect()
        'conn.Open()
        'cmd = New SqlCommand("select * from Usert where UName='" + TextBox1.Text + "' and pass='" + TextBox2.Text + "'", conn)
        'dt = New DataTable()
        'da = New SqlDataAdapter(cmd)
        'da.Fill(dt)
        'x = Convert.ToInt32(dt.Rows.Count.ToString())
        'If (x = 0) Then
        '    MsgBox("User Name or Password Incorrect !", vbOK, "The Garden Cafe")
        '    Me.Hide()
        '    Me.Show()
        '    Clr()
        'Else
        '    MsgBox("Successfully Login in to system !", vbOK, "The Garden Cafe")
        '    Me.Hide()
        '    Form2.Show()
        'End If
        'conn.Close()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub BunifuMaterialTextbox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BunifuMaterialTextbox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            BunifuMaterialTextbox2.Focus()
        End If
    End Sub

    Private Sub BunifuMaterialTextbox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BunifuMaterialTextbox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            BunifuThinButton21.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
    Private Sub Clr()
        BunifuMaterialTextbox1.Text = ""
        BunifuMaterialTextbox2.Text = ""
    End Sub

    Private Sub BunifuThinButton21_Click(sender As Object, e As EventArgs) Handles BunifuThinButton21.Click
        UsrInfo()

        setname = BunifuMaterialTextbox1.Text
        setdiv = div
        setempname = empnm

        conn = GetConnect()
        conn.Open()
        cmd = New SqlCommand("select * from Usert where UName='" + BunifuMaterialTextbox1.Text + "' and pass='" + BunifuMaterialTextbox2.Text + "'", conn)
        dt = New DataTable()
        da = New SqlDataAdapter(cmd)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If (x = 0) Then
            MsgBox("User Name or Password Incorrect !", vbOK, "The Garden Cafe")
            Me.Hide()
            Me.Show()
            Clr()
        Else
            MsgBox("Successfully Login in to system !", vbOK, "The Garden Cafe")
            Me.Hide()
            Control.Show()
        End If
        conn.Close()
    End Sub

    Private Sub BunifuMaterialTextbox1_OnValueChanged(sender As Object, e As EventArgs) Handles BunifuMaterialTextbox1.OnValueChanged

    End Sub

    Public Sub UsrInfo()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT emp_code,div FROM Usert  where UName='" + BunifuMaterialTextbox1.Text + "' and pass='" + BunifuMaterialTextbox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                Label6.Text = dr.GetValue(0).ToString()
                div = dr.GetValue(1).ToString()
            End While
        Catch ex As Exception

        End Try
        conn.Close()

        conn = GetConnect()
        conn.Open()
        SQL = "SELECT Name FROM Employee  where empid='" + Label6.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        Try
            dr1 = cmd.ExecuteReader
            While dr1.Read
                empnm = dr1.GetValue(0).ToString()
            End While
        Catch ex As Exception

        End Try
        conn.Close()
        'conn = GetConnect()
        'conn.Open()
        'SQL = "select emp_code from Usert where UName='" + BunifuMaterialTextbox1.Text + "' and pass='" + BunifuMaterialTextbox2 + "'"
    End Sub
End Class
