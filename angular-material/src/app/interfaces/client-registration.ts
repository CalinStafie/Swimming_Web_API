import { AuthModel } from "./auth-model";
import { ClientModel } from "./client-model";

export interface ClientRegistrationModel {
    registerModel: AuthModel;
    clientDTO: ClientModel;
}
