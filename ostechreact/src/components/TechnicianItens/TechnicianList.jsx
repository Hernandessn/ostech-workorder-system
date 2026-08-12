import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const TechnicianList = ({ technician, onEdit, onDelete }) => {
    return (
        <li className="flex items-center justify-between gap-4 px-4 py-3 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div>
                <p className="font-medium">{technician.name}</p>
                <p className="text-sm text-[#6EACDA]">{technician.specialty}</p>
                <p className="text-sm text-[#6EACDA]">{technician.contact}</p>
                <p className="text-sm text-[#6EACDA]">{technician.availability}</p>
                <p className="text-sm text-[#6EACDA]">{technician.hiringDate}</p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}