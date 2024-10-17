export interface ResponseReport{
      id: number,
      appointmentID: number,
      text: string,
      doctorID: string
      doctorFirstName:string,
      doctorLastName: string,
      doctorTitle: string,
      doctorBranch:string
      patientID: number,
      patientFirstName: string,
      patientLastName: string
      patientIdentity:string,
      appointmentDate: string,
      appointmentTime: string,
      reportDate:Date

    }
