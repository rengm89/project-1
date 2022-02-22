Imports System.IO
Public Class frmSistemaLibreria
    Private Sub frmSistemaLibreria_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()

        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Idioma"


        Adaptador = New OleDb.OleDbDataAdapter(Comando)
        DS = New DataSet
        Adaptador.Fill(DS)

        cmbIdioma.DataSource = DS.Tables(0)
        cmbIdioma.ValueMember = "IdIdioma"
        cmbIdioma.DisplayMember = "Nombre"

        Conexion.Close()
    End Sub

    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim cantidad As Integer = 0
        Dim total As Decimal = 0


        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()

        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Libro"

        DR = Comando.ExecuteReader
        dgvDatos.Rows.Clear()

        If DR.HasRows Then
            Do While DR.Read

                If DR("IdIdioma") = cmbIdioma.SelectedValue Then
                    dgvDatos.Rows.Add(DR("Titulo"), DR("Año"), DR("Cantidad"), DR("Precio"), (DR("Precio") * DR("Cantidad")))

                    cantidad = cantidad + 1
                    total = total + (DR("Precio") * DR("Cantidad"))

                End If
            Loop


        End If
        lblCantidad.Text = cantidad
        lblTotal.Text = total

        Conexion.Close()
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim objArchivo As New StreamWriter("DatosExportados8.csv", False, System.Text.Encoding.UTF8)
        objArchivo.WriteLine("Libería")
        objArchivo.WriteLine("")
        objArchivo.WriteLine("Título;Año;Cantidad;Precio;Precio total")
        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()

        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Libro"

        DR = Comando.ExecuteReader
        dgvDatos.Rows.Clear()

        If DR.HasRows Then
            Do While DR.Read


                If DR("IdIdioma") = cmbIdioma.SelectedValue Then
                    objArchivo.Write(DR("Titulo"))
                    objArchivo.Write(DR("Año"))
                    objArchivo.Write(DR("Cantidad"))
                    objArchivo.Write(DR("Precio"))
                    objArchivo.WriteLine(DR("Precio") * DR("Cantidad"))

                End If
            Loop

        End If
        objArchivo.Close()
        Conexion.Close()
        MessageBox.Show("Datos Exportados")
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        DialogoImpresora.ShowDialog()
        DocumentoImprimir.PrinterSettings = DialogoImpresora.PrinterSettings
        DocumentoImprimir.Print()
    End Sub

    Private Sub DocumentoImprimir_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles DocumentoImprimir.PrintPage
        Dim LetraTitulo As New Font("Arial", 14)
        Dim LetraTituloColumna As New Font("Arial", 12)
        Dim TipoLetra As New Font("Arial", 10)
        Dim fila As Integer = 100
        Dim titulo As String = "Idioma: " & cmbIdioma.Text



        e.Graphics.DrawString(titulo, LetraTitulo, Brushes.Blue, 80, 40)
        e.Graphics.DrawString("Titulo del Libro", LetraTituloColumna, Brushes.Black, 80, 80)
        e.Graphics.DrawString("Cantidad", LetraTituloColumna, Brushes.Black, 380, 80)
        e.Graphics.DrawString("Precio", LetraTituloColumna, Brushes.Black, 480, 80)
        e.Graphics.DrawString("Precio Total", LetraTituloColumna, Brushes.Black, 580, 80)


        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()

        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Libro"

        DR = Comando.ExecuteReader
        dgvDatos.Rows.Clear()

        If DR.HasRows Then
            Do While DR.Read

                If DR("IdIdioma") = cmbIdioma.SelectedValue Then

                    e.Graphics.DrawString(DR("Titulo"), TipoLetra, Brushes.Black, 80, fila)
                    e.Graphics.DrawString(DR("Cantidad"), TipoLetra, Brushes.Black, 380, fila)
                    e.Graphics.DrawString(DR("Precio"), TipoLetra, Brushes.Black, 480, fila)
                    e.Graphics.DrawString((DR("Precio") * DR("Cantidad")), TipoLetra, Brushes.Black, 580, fila)

                    fila = fila + 15

                End If

            Loop

            e.Graphics.DrawString("Cantidad de datos: " & lblCantidad.Text, TipoLetra, Brushes.Black, 80, 550)
            e.Graphics.DrawString("Importe total: " & lblTotal.Text, TipoLetra, Brushes.Black, 80, 570)
        End If


        Conexion.Close()
    End Sub
End Class
