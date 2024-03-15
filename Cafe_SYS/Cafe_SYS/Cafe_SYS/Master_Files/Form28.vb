Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form28
    Dim str, x As Integer
    Private Sub Form28_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT max(stores)stores FROM TGC"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                str = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception

        End Try
        conn.Close()
        ''
        TextBox3.Text = str + 1
        ''
        displaydata()
        ''
        grd()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Stores"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        Me.KeyPreview = True
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox7.Focus()
        End If
    End Sub
    Private Sub displaydata()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name as Store from Stores", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 85
        BunifuCustomDataGrid1.Columns(1).Width = 200
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Invalid Store !")
            TextBox1.Text = ""
            TextBox1.Focus()
            Exit Sub
        End If
        ''
        If TextBox7.Text = "" Then
            MsgBox("Invalid Code !")
            TextBox7.Text = ""
            TextBox7.Focus()
            Exit Sub
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Stores where Name = '" + TextBox1.Text + "' or Code = '" + TextBox7.Text + "' "
        cmd1 = New SqlCommand(SQL, conn)
        dt = New DataTable
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If x > 0 Then
            MsgBox("Store Already Exist !")
            TextBox1.Text = ""
            TextBox7.Text = ""
            TextBox1.Focus()
            Exit Sub
        Else
            conn = GetConnect()
            conn.Open()
            SQL = "update TGC set [stores] = [stores] + 1"
            cmd3 = New SqlCommand(SQL, conn)
            cmd3.ExecuteNonQuery()
            conn.Close()
            ''
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Stores (ID,Code,Name) values('" + TextBox3.Text + "','" + TextBox7.Text + "','" + TextBox1.Text + "')"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            conn.Close()
        End If
        conn.Close()
        MsgBox("Update Successfully !")
        ''
        displaydata()
        ''
        Clr()
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select ID from Stores where Name ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox4.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox4.Text = "" Then
            MsgBox("Invalid Store !")
            Exit Sub
        End If
        ''
        If TextBox2.Text = "" Then
            MsgBox("Inavlid New Store !")
            TextBox2.Focus()
            Exit Sub
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Stores where Name = '" + TextBox2.Text + "' "
        cmd1 = New SqlCommand(SQL, conn)
        dt = New DataTable
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If x > 0 Then
            MsgBox("Store Already Exist !")
            TextBox2.Text = ""
            TextBox2.Focus()
            Exit Sub
        Else
            conn = GetConnect()
            conn.Open()
            SQL = "update Stores set Name = '" + TextBox2.Text + "' where ID = '" + TextBox4.Text + "'"
            cmd3 = New SqlCommand(SQL, conn)
            cmd3.ExecuteNonQuery()
            conn.Close()
        End If
        MsgBox("Update Successfully !")
        ''
        displaydata()
        ''
        Clr()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox4.Text = "" Then
            MsgBox("Invalid Store !")
            Exit Sub
        End If
        ''
        If TextBox5.Text = "" Then
            MsgBox("Inavlid New Code !")
            TextBox5.Focus()
            Exit Sub
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Stores where Code = '" + TextBox5.Text + "' "
        cmd1 = New SqlCommand(SQL, conn)
        dt = New DataTable
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If x > 0 Then
            MsgBox("Code Already Exist !")
            TextBox5.Text = ""
            TextBox5.Focus()
            Exit Sub
        Else
            conn = GetConnect()
            conn.Open()
            SQL = "update Stores set Code = '" + TextBox5.Text + "' where ID = '" + TextBox4.Text + "'"
            cmd3 = New SqlCommand(SQL, conn)
            cmd3.ExecuteNonQuery()
            conn.Close()
        End If
        MsgBox("Update Successfully !")
        ''
        displaydata()
        ''
        Clr()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox4.Text = "" Then
            MsgBox("Invalid Store !")
            Exit Sub
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "delete from Stores where ID = '" + TextBox4.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Update Successfully !")
        conn.Close()
        Clr()
    End Sub

    Private Sub Form28_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.F3) Then
            Clr()
        End If
    End Sub

    Private Sub Clr()
        Dim form28 As New Form28
        Me.Hide()
        form28.Show()
    End Sub
End Class