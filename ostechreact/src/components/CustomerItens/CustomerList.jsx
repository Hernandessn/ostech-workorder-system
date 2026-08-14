import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const CustomerList = ({ customer, onEdit, onDelete }) => {
    return (
        <li className="flex items-start sm:items-center justify-between gap-4 px-4 py-4 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div className="flex flex-col gap-1">
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Name: </span>
                    {customer.name}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Email: </span>
                    {customer.email}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Phone: </span>
                    {customer.phone}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Document: </span>
                    {customer.document}
                </p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}