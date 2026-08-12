import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const TechnicianList = ({ technician, onEdit, onDelete }) => {
    return (
        <li className="flex items-center justify-between gap-4 px-4 py-3 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div>
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Name: </span>
                    {technician.name}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Speciality: </span>
                    {technician.specialty}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Contact: </span>

                    {technician.contact}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Availability: </span>
                    <span className={technician.availability ? 'text-green-400' : 'text-red-400'}>
                        {technician.availability ? 'Available' : 'Unavailable'}
                    </span>
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Hiring Date: </span>
                    {technician.hiringDate}
                </p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}