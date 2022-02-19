import { ReceptionistDTO } from "./receptionist-dto";
import { ClientDTO } from "./client-dto";
import { SubscriptionDTO } from "./subscription-dto";

export interface PurchaseInformationModel {
    clientDTO: ClientDTO;
    receptionistDTO: ReceptionistDTO;
    subscriptionDTO: SubscriptionDTO;
    startTime: Date;
    endTime: Date;
}
