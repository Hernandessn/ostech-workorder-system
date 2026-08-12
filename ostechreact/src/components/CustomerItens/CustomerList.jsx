import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const CustomerList = ({ customer, onEdit, onDelete }) => {
    return (
        <li className="flex items-center justify-between gap-4 px-4 py-3 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div>
                <p className="font-medium">{customer.name}</p>
                <p className="text-sm text-[#6EACDA]">{customer.description}</p>
                <p className="text-sm text-[#6EACDA]">{customer.email}</p>
                <p className="text-sm text-[#6EACDA]">{customer.phone}</p>
                <p className="text-sm text-[#6EACDA]">{customer.document}</p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}