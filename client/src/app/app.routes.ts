import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { MemberList } from '../features/members/member-list/member-list';
import { MemberDetail } from '../features/members/member-detail/member-detail';
import { Lists } from '../features/lists/lists';
import { Messages } from '../features/messages/messages';
import { authGuard } from '../core/guard/auth-guard';
import { TestErrors } from '../features/test-errors/test-errors';

//Indicamos las rutas a donde debera de ir nuestra app
//Debemos indicar que parte se debe recargar
export const routes: Routes = [
    { path: "", component: Home },//ruta vacia y principal
    {
        path: "",
        runGuardsAndResolvers: "always",
        canActivate: [authGuard],
        children: [
            { path: "members", component: MemberList, canActivate: [authGuard] },
            { path: "members/{id}", component: MemberDetail },
            { path: "lists", component: Lists },
            { path: "messages", component: Messages }            
        ]
    },
    { path: "errors", component: TestErrors }, //Manejo de errores 
    { path: "**", component: Home } //Cuando no hace match con ninguna
];
