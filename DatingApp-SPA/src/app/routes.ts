import { MessagesResolver } from './_resolvers/messages.resolver';
import { ListsResolver } from './_resolvers/lists.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { AuthGuard } from './_guards/auth.guard';
import { ListsComponent } from './lists/lists.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MessagesComponent } from './messages/messages.component';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MemberListComponent, resolve: { users: MemberListResolver } },
            { path: 'members/:id', component: MemberDetailComponent, resolve: { user: MemberDetailResolver } },
            { path: 'messages', component: MessagesComponent, resolve: { messages: MessagesResolver } },
            { path: 'lists', component: ListsComponent, resolve: { users: ListsResolver } },
            { path: 'admin', component: AdminPanelComponent, data: { roles: ['Admin', 'Moderator'] } },
            {
                path: 'member/edit', component: MemberEditComponent, resolve: { user: MemberEditResolver },
                canDeactivate: [PreventUnsavedChanges]
            }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
