Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form71
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1, cmd2, cmd3 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr, dr1, dr2, dr3 As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Sub_Cat2 where Name ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox4.Text = dr.GetValue(0).ToString
            TextBox5.Text = dr.GetValue(1).ToString
            TextBox8.Text = dr.GetValue(3).ToString
        End While
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Main_Cat2 where code ='" + TextBox8.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox4.Text = dr.GetValue(2).ToString
        End While
    End Sub

    Private Sub ComboBox4_Click(sender As Object, e As EventArgs) Handles ComboBox4.Click
        ComboBox3.Enabled = True
        If ComboBox3.Enabled = True Then
            ComboBox4.Visible = False

            TextBox8.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox8.Text = "" And TextBox9.Text = "" And TextBox2.Text = "" Then

            MsgBox("Invalid Details !")
            ComboBox3.Focus()

        ElseIf TextBox8.Text = "" And TextBox9.Text = "" And TextBox2.Text <> "" Then
            MsgBox("Invalid Main Category !")
            ComboBox3.Focus()

        ElseIf TextBox8.Text <> "" Or TextBox9.Text <> "" And TextBox2.Text = "" Then

            If TextBox8.Text <> "" And TextBox9.Text = "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "update Sub_Cat2 set M_cat='" + TextBox8.Text + "' where Id='" + TextBox4.Text + "' " &
                    "and code = '" + TextBox5.Text + "' and Name = '" + ComboBox3.Text + "'"
                cmd = New SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Updated !")
                conn.Close()

            ElseIf TextBox9.Text <> "" And TextBox8.Text = "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "update Sub_Cat2 set M_cat='" + TextBox9.Text + "' where Id='" + TextBox4.Text + "' " &
                    "and code = '" + TextBox5.Text + "' and Name = '" + ComboBox1.Text + "'"
                cmd = New SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Updated !")
                conn.Close()
            End If

        ElseIf TextBox8.Text <> "" Or TextBox9.Text <> "" And TextBox2.Text <> "" Then

            If TextBox8.Text <> "" And TextBox2.Text <> "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "update Sub_Cat2 set Name='" + TextBox2.Text + "' , M_cat='" + TextBox8.Text + "' where Id='" + TextBox4.Text + "' " &
                    "and code = '" + TextBox5.Text + "'"
                cmd = New SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Updated !")
                conn.Close()

            ElseIf TextBox9.Text <> "" And TextBox2.Text <> "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "update Sub_Cat2 set Name='" + TextBox2.Text + "' , M_cat='" + TextBox9.Text + "' where Id='" + TextBox4.Text + "' " &
                    "and code = '" + TextBox5.Text + "'"
                cmd = New SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Updated !")
                conn.Close()
            End If

        End If

        'conn = GetConnect()
        'conn.Open()
        'SQL = "update Sub_Cat2 set Name = '" + TextBox2.Text + "' , M_cat='" + TextBox8.Text + "' where Id='" + TextBox4.Text + "' " &
        '    "and code = '" + TextBox5.Text + "'"
        'cmd = New SqlCommand(SQL, conn)
        'cmd.ExecuteNonQuery()
        'MsgBox("Successfully Updated !")
        'conn.Close()

        Dim form71 As New Form71
        Me.Hide()
        form71.Show()
    End Sub

    Private Sub Form61_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.F3) Then
            Dim form71 As New Form71
            Me.Hide()
            form71.Show()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn = GetConnect()
        conn.Open()
        SQL = "delete from Sub_Cat2 where Id='" + TextBox4.Text + "' " &
            "and code = '" + TextBox5.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Successfully Deleted !")
        conn.Close()
        Dim form71 As New Form71
        Me.Hide()
        form71.Show()
    End Sub

    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Main_Cat2 where Name ='" + ComboBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox9.Text = dr.GetValue(1).ToString
        End While
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Main_Cat2 where Name ='" + ComboBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox7.Text = dr.GetValue(1).ToString
        End While
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox6.Text = i.ToString()

        TextBox3.Text = "SC/0" + TextBox6.Text

        '''''''''''''''''''''''''''''
        If Not ComboBox2.Text = "" Then
            If Not TextBox1.Text = "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "insert into Sub_Cat2(id,code,Name,M_cat) values ('" + TextBox6.Text + "', " &
            " '" + TextBox3.Text + "' , '" + TextBox1.Text + "' , '" + TextBox7.Text + "'  )"
                cmd = New SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Saved !")
                conn.Close()
            Else
                MsgBox("Invalid Sub Category !")
            End If
        Else
            MsgBox("Invalid Main Category !")
        End If
        Dim form71 As New Form71
        Me.Hide()
        form71.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub FillCombo()

        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Sub_Cat2"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat2"
        cmd1 = New SqlCommand(SQL, conn)
        dr1 = cmd1.ExecuteReader
        While (dr1.Read())
            ComboBox2.Items.Add(dr1.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat2"
        cmd2 = New SqlCommand(SQL, conn)
        dr2 = cmd2.ExecuteReader
        While (dr2.Read())
            ComboBox3.Items.Add(dr2.GetString(0))
        End While
        conn.Close()
        ''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat2"
        cmd3 = New SqlCommand(SQL, conn)
        dr3 = cmd3.ExecuteReader
        While (dr3.Read())
            ComboBox4.Items.Add(dr3.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub Form61_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCombo()
        '''''''''''''
        Me.KeyPreview = True
        ''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Count(Id) from Sub_Cat2"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox6.Text = dr.GetValue(0).ToString
            End While
        Catch ex As Exception
        End Try
        conn.Close()
    End Sub
End Class




